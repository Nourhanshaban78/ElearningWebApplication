using E_Learning.Core.Base;
using MediatR;

namespace E_Learning.Core.Features.Quizzes.Commands.SaveAnswer
{
    public record SaveAnswerCommand(
        int AttemptId,
        int QuestionId,
        int? SelectedOptionId,
        string? AnswerText
    ) : IRequest<Response<string>>;
}