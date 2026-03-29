using AutoMapper;
using E_Learning.Core.Base;
using E_Learning.Core.Entities.Courses;
using E_Learning.Core.Repository;
using E_Learning.Service.DTOs.Lesson;
using E_Learning.Service.DTOs.Section;
using FFMpegCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.Services.Courses
{
    public class CourseContentService : ICourseContentService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _response;
        private readonly IWebHostEnvironment _webHost;

        public CourseContentService(IUnitOfWork unit, IMapper mapper, ResponseHandler response, IWebHostEnvironment webHost)
        {
            _unit = unit;
            _mapper = mapper;
            _response = response;
            _webHost = webHost;
        }

        private async Task<(string Path, int Duration)> UploadVideoAndGetDurationAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads", "videos");
            if(!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var videoInfo = await FFProbe.AnalyseAsync(filePath);
            int duration = (int)videoInfo.Duration.TotalSeconds;

            return ($"/uploads/videos/{fileName}",duration);
        }
        #region Section
        
        public async Task<Response<SectionDto>> CreateSectionAsync(int courseId, CreateSectionDto dto, CancellationToken ct = default)
        {
            var course = await _unit.Courses.GetByIdAsync(courseId);

            if (course == null)
                return _response.NotFound<SectionDto>("Course Not Found");

            var section = _mapper.Map<Section>(dto);

            section.CourseId = courseId;

            await _unit.Sections.AddAsync(section);

            await _unit.SaveChangesAsync();

            var result = _mapper.Map<SectionDto>(section);

            return _response.Created(result);
        }

        public async Task<Response<SectionDto>> UpdateSectionAsync(int sectionId, UpdateSectionDto dto, CancellationToken ct = default)
        {
            var section = await _unit.Sections.GetByIdAsync(sectionId);

            if (section == null)
                return _response.NotFound<SectionDto>("Section Not Found");

            section.Title = dto.Title;
            section.OrderIndex = dto.OrderIndex;

            _unit.Sections.Update(section);

            await _unit.SaveChangesAsync();

            var result = _mapper.Map<SectionDto>(section);

            return _response.Success(result);
        }

        public async Task<Response<string>> DeleteSectionAsync(int sectionId, CancellationToken ct = default)
        {
            var section = await _unit.Sections.GetByIdAsync(sectionId);

            if (section == null)
                return _response.NotFound<string>("Section Not Found");

            _unit.Sections.Remove(section);

            await _unit.SaveChangesAsync();

            return _response.Deleted<string>();
        }

        public async Task<Response<IReadOnlyList<SectionDto>>> GetSectionsByCourseIdAsync(int courseId, CancellationToken ct = default)
        {
            var course = await _unit.Courses.GetByIdAsync(courseId);

            if (course == null)
                return _response.NotFound<IReadOnlyList<SectionDto>>("Course Not Found");

            var sections = await _unit.Sections
                .FindAsync(x => x.CourseId == courseId, ct);

            var result = _mapper.Map<IReadOnlyList<SectionDto>>(sections);

            return _response.Success(result);
        }
        #endregion

        #region Lesson
        public async Task<Response<LessonDto>> CreateLessonAsync(int sectionId, CreateLessonDto dto, CancellationToken ct = default)
        {
            var section = await _unit.Sections.GetByIdAsync(sectionId);

            if (section == null)
                return _response.NotFound<LessonDto>("Section Not Found");

            var lesson = _mapper.Map<Lesson>(dto);
            lesson.SectionId = sectionId;

            if (dto.VideoUrl != null)
                {
                    var uploadResult = await UploadVideoAndGetDurationAsync(dto.VideoUrl);
                    lesson.VideoUrl = uploadResult.Path;
                    lesson.DurationSeconds = uploadResult.Duration;
                }

            await _unit.Lessons.AddAsync(lesson);
            await _unit.SaveChangesAsync();

            var result = _mapper.Map<LessonDto>(lesson);

            return _response.Created(result);
        }

        public async Task<Response<LessonDto>> UpdateLessonAsync(int lessonId, UpdateLessonDto dto, CancellationToken ct = default)
        {
            var lesson = await _unit.Lessons.GetByIdAsync(lessonId);

            if (lesson == null)
                return _response.NotFound<LessonDto>("Lesson Not Found");

            _mapper.Map(dto, lesson);

            if (dto.VideoUrl != null)
            {
                if(!string.IsNullOrEmpty( lesson.VideoUrl))
                {
                    var oldRelativePath = lesson.VideoUrl.TrimStart('/');
                    var oldFillPath = Path.Combine(_webHost.WebRootPath, oldRelativePath);

                    if(System.IO.File.Exists(oldFillPath))
                        System.IO.File.Delete(oldFillPath);
                }
                var uploadResult = await UploadVideoAndGetDurationAsync(dto.VideoUrl);
                lesson.VideoUrl = uploadResult.Path;
                lesson.DurationSeconds = uploadResult.Duration;
            }



            _unit.Lessons.Update(lesson);
            await _unit.SaveChangesAsync();

            var result = _mapper.Map<LessonDto>(lesson);

            return _response.Success(result);
        }

        public async Task<Response<string>> DeleteLessonAsync(int lessonId, CancellationToken ct = default)
        {
            var lesson = await _unit.Lessons.GetByIdAsync(lessonId);

            if (lesson == null)
                return _response.NotFound<string>("Lesson Not Found");

            if (!string.IsNullOrEmpty(lesson.VideoUrl))
            {
                var filePath = Path.Combine(_webHost.WebRootPath, lesson.VideoUrl.TrimStart('/'));

                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {                        
                    }
                }
            }
            _unit.Lessons.Remove(lesson);
            await _unit.SaveChangesAsync();

            return _response.Deleted<string>();
        }

        public async Task<Response<IReadOnlyList<LessonDto>>> GetLessonsBySectionIdAsync(int sectionId, CancellationToken ct = default)
        {
            var section = await _unit.Sections.GetByIdAsync(sectionId);

            if (section == null)
                return _response.NotFound<IReadOnlyList<LessonDto>>("Section Not Found");

            var lessons = await _unit.Lessons
                .FindAsync(x => x.SectionId == sectionId, ct);

            var result = _mapper.Map<IReadOnlyList<LessonDto>>(lessons);

            return _response.Success(result);
        }

        public async Task<Response<IReadOnlyList<LessonDto>>> GetLessonsByCourseIdAsync(int courseId, CancellationToken ct = default)
        {
            var course = await _unit.Courses.GetByIdAsync(courseId);

            if (course == null)
                return _response.NotFound<IReadOnlyList<LessonDto>>("Course Not Found");

            var lessons = await _unit.Lessons
                .FindAsync(x => x.Section.CourseId == courseId,ct);

            var result = _mapper.Map<IReadOnlyList<LessonDto>>(lessons);

            return _response.Success(result);
        }
        #endregion 

    }
}
