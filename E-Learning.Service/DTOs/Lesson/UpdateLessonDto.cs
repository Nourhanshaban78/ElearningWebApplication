using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.Lesson
{
    public class UpdateLessonDto
    {
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public IFormFile? VideoUrl { get; set; }
        public string? Content { get; set; }


        public int OrderIndex { get; set; }

        public bool IsFreePreview { get; set; }
    }
}

