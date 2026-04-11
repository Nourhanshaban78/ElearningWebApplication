
using E_Learning.Core.Base;
using MediatR;


namespace E_Learning.Core.Features.Quizzes.Commands.StartQuizAttempt
{
    public record StartQuizAttemptCommand(int QuizId)
        : IRequest<Response<StartQuizAttemptResponse>>;
}