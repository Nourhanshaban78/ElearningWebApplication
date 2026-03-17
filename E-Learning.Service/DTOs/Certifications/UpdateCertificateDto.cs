using System.ComponentModel.DataAnnotations;

public class UpdateCertificateDto
{
    [StringLength(100, MinimumLength = 3)]
    public string? CertificateCode { get; set; }

    public DateTime? IssuedAt { get; set; }

    [Url]
    public string? FileUrl { get; set; }
}