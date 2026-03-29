using E_Learning.Service.DTOs.Lesson;
using E_Learning.Service.DTOs.Section;
using E_Learning.Service.Services.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.API.Controllers.Courses
{
    [Authorize(Roles = "Instructor,Admin")]
    [ApiController]
    public class CourseContentController : ControllerBase
    {
        private readonly ICourseContentService _service;

        public CourseContentController(ICourseContentService service)
        {
            _service = service;
        }
        #region Section
        [HttpPost("courses/{courseId}/sections")]
        public async Task<IActionResult> CreateSection(int courseId, CreateSectionDto dto, CancellationToken ct = default)
        {
            var result = await _service.CreateSectionAsync(courseId, dto);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpPut("sections/{sectionId}")]
        public async Task<IActionResult> UpdateSection(int sectionId, UpdateSectionDto dto, CancellationToken ct = default)
        {
            var result = await _service.UpdateSectionAsync(sectionId, dto);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpDelete("sections/{sectionId}")]
        public async Task<IActionResult> DeleteSection(int sectionId, CancellationToken ct = default)
        {
            var result = await _service.DeleteSectionAsync(sectionId);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpGet("courses/{courseId}/sections")]
        public async Task<IActionResult> GetSections(int courseId, CancellationToken ct = default)
        {
            var result = await _service.GetSectionsByCourseIdAsync(courseId);
            return StatusCode((int)result.HttpStatusCode, result);
        }
#endregion


        #region Lesson
        [HttpPost("sections/{sectionId}/lessons")]
        public async Task<IActionResult> CreateLesson(int sectionId,[FromForm] CreateLessonDto dto, CancellationToken ct = default)
        {
            var result = await _service.CreateLessonAsync(sectionId, dto, ct);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpPut("lessons/{lessonId}")]
        public async Task<IActionResult> UpdateLesson(int lessonId,[FromForm] UpdateLessonDto dto, CancellationToken ct = default)
        {
            var result = await _service.UpdateLessonAsync(lessonId, dto, ct);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpDelete("lessons/{lessonId}")]
        public async Task<IActionResult> DeleteLesson(int lessonId, CancellationToken ct = default)
        {
            var result = await _service.DeleteLessonAsync(lessonId);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpGet("sections/{sectionId}/lessons")]
        public async Task<IActionResult> GetLessonsBySection(int sectionId, CancellationToken ct = default)
        {
            var result = await _service.GetLessonsBySectionIdAsync(sectionId);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpGet("courses/{courseId}/lessons")]
        public async Task<IActionResult> GetLessonsByCourse(int courseId, CancellationToken ct = default)
        {
            var result = await _service.GetLessonsByCourseIdAsync(courseId);
            return StatusCode((int)result.HttpStatusCode, result);
        }
        #endregion
    }
}
