using E_Learning.Core.Features.Quizzes.Commands.StartQuizAttempt;
using E_Learning.Core.Features.Quizzes.Commands.SubmitQuizAttempt;
using E_Learning.Core.Features.Quizzes.Commands.SaveAnswer;
using E_Learning.Core.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace E_Learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // بس الطلاب المسموح لهم
    public class QuizAttemptsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuizAttemptsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 🟢 Start Attempt
        [HttpPost("start")]
        public async Task<IActionResult> StartAttempt([FromBody] StartQuizAttemptCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return HandleResponse(result);
        }

        // 🟢 Save Answer
        [HttpPost("save-answer")]
        public async Task<IActionResult> SaveAnswer([FromBody] SaveAnswerCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return HandleResponse(result);
        }

        // 🟢 Submit Attempt
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitAttempt([FromBody] SubmitQuizAttemptCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return HandleResponse(result);
        }

        // ✅ Helper method to handle Response<T>
        private IActionResult HandleResponse<T>(Response<T> response)
        {
            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}