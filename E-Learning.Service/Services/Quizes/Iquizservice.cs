using E_Learning.Core.Base;
using E_Learning.Service.DTOs;


namespace E_Learning.Service.Services.QuizServices
{
    public interface IQuizService
    {
        Task<Response<QuizDetailResponseDto>> CreateAsync(CreateQuizDto dto, Guid instructorId, bool isAdmin, CancellationToken ct = default);
        Task<Response<QuizDetailResponseDto>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Response<IReadOnlyList<QuizResponseDto>>> GetAllAsync(CancellationToken ct = default);
        Task<Response<IReadOnlyList<QuizResponseDto>>> GetByCourseIdAsync(int courseId, CancellationToken ct = default);
        Task<Response<QuizResponseDto>> UpdateAsync(int id, UpdateQuizDto dto, Guid instructorId, bool isAdmin, CancellationToken ct = default);
        Task<Response<string>> DeleteAsync(int id, Guid instructorId, bool isAdmin, CancellationToken ct = default);
    }
}
