using E_Learning.Core.Base;


namespace E_Learning.Core.Entities.Courses
{
    public class Lesson : BaseEntity
    {
        public int SectionId { get; set; }
        public Section Section { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Video, Article, Quiz
        public string? VideoUrl { get; set; }
        public string? Content { get; set; }
        public int? DurationSeconds { get; set; }
        public int OrderIndex { get; set; }
        public bool IsFreePreview { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
