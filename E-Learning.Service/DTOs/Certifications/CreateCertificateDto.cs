using System.ComponentModel.DataAnnotations;

public class CreateCertificateDto
{
    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string CertificateCode { get; set; } = string.Empty;

    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

    [Url]
    public string? FileUrl { get; set; }
}