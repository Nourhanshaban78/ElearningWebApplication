using E_Learning.Service.DTOs;
using E_Learning.Service.Services.QuizServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Learning.API.Controllers
{
    [ApiController]
    [Route("api/quizzes")]
    [Authorize]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        // GET api/quizzes
        [HttpGet]
        [Authorize(Roles = "Student,Instructor,Admin")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var response = await _quizService.GetAllAsync(ct);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        // GET api/quizzes/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Student,Instructor,Admin")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var response = await _quizService.GetByIdAsync(id, ct);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        // GET api/quizzes/course/{courseId}
        [HttpGet("course/{courseId}")]
        [Authorize(Roles = "Student,Instructor,Admin")]
        public async Task<IActionResult> GetByCourse(int courseId, CancellationToken ct)
        {
            var response = await _quizService.GetByCourseIdAsync(courseId, ct);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        // POST api/quizzes
        [HttpPost]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Create([FromBody] CreateQuizDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var instructorId))
                return Unauthorized();
            var isAdmin = User.IsInRole("Admin"); // ← هنا
            var response = await _quizService.CreateAsync(dto, instructorId, isAdmin, ct); // ← وهنا
            return StatusCode((int)response.HttpStatusCode, response);
        }

        // PUT api/quizzes/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateQuizDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var instructorId))
                return Unauthorized();
            var isAdmin = User.IsInRole("Admin"); // ← هنا
            var response = await _quizService.UpdateAsync(id, dto, instructorId, isAdmin, ct); // ← وهنا
            return StatusCode((int)response.HttpStatusCode, response);
        }

        // DELETE api/quizzes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var instructorId))
                return Unauthorized();
            var isAdmin = User.IsInRole("Admin"); // ← هنا
            var response = await _quizService.DeleteAsync(id, instructorId, isAdmin, ct); // ← وهنا
            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}