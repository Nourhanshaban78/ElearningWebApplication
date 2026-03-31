using E_Learning.Core.Base;
using MediatR;
using System.Collections.Generic;

namespace E_Learning.Core.Features.Quizzes.Commands.SubmitQuizAttempt
{
    public record SubmitQuizAttemptCommand(int AttemptId, List<QuizAnswerDto> Answers)
     : IRequest<Response<SubmitQuizAttemptResponse>>;

    // DTO لتمثيل إجابة كل سؤال
    public class QuizAnswerDto
    {
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }            // Single choice / True-False
        public List<int>? SelectedOptionIds { get; set; }     // Multiple choice
        public string? TextAnswer { get; set; }              // Essay / Text
    }
}