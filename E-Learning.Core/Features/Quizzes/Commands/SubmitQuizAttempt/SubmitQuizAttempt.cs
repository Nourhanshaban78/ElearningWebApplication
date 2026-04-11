using E_Learning.Core.Base;
using MediatR;
using System.Collections.Generic;

namespace E_Learning.Core.Features.Quizzes.Commands.SubmitQuizAttempt
{
    public record SubmitQuizAttemptCommand(int AttemptId)
       : IRequest<Response<SubmitQuizAttemptResponse>>;


}