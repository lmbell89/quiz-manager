using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizManager.Api.Auth;
using QuizManager.Api.Contracts;
using QuizManager.Core.BusinessLogic;

namespace QuizManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IQuizCoordinator _quizCoordinator;

        public QuizController(IMapper mapper, IQuizCoordinator quizCoordinator)
        {
            _mapper = mapper;
            _quizCoordinator = quizCoordinator;
        }

        [HttpGet]
        [Authorize(Permissions.ReadQuiz)]
        [Authorize(Permissions.ReadQuestion)]
        public IActionResult GetQuizzes(int page, int pageSize, string title)
        {
            var quizModels = _quizCoordinator.GetQuizzes(pageSize, page, title);
            var quizzes = _mapper.Map<QuizSummary[]>(quizModels);
            return Ok(quizzes);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Permissions.ReadQuiz)]
        [Authorize(Permissions.ReadQuestion)]
        [Authorize(Permissions.ReadAnswer)]
        public IActionResult GetQuizById(int id)
        {
            var quizModel = _quizCoordinator.GetQuizById(id);
            var quiz = _mapper.Map<Quiz>(quizModel);
            return Ok(quiz);
        }

        [HttpGet]
        [Route("summary/{id}")]
        [Authorize(Permissions.ReadQuiz)]
        [Authorize(Permissions.ReadQuestion)]
        public IActionResult GetQuizSummaryById(int id)
        {
            var quizModel = _quizCoordinator.GetQuizById(id);
            var quiz = _mapper.Map<QuizSummary>(quizModel);
            return Ok(quiz);
        }

        [HttpPost]
        [Authorize(Permissions.CreateQuiz)]
        public IActionResult CreateQuiz([FromBody] QuizTitle title)
        {
            var quizModel = _quizCoordinator.CreateQuiz(title.Title);
            var quiz = _mapper.Map<Quiz>(quizModel);
            return Ok(quiz);
        }

        [HttpPatch]
        [Route("{id}")]
        [Authorize(Permissions.UpdateQuiz)]
        public IActionResult UpdateTitle(int id, [FromBody] QuizTitle title)
        {
            var quizModel = _quizCoordinator.UpdateTitle(id, title.Title);
            var quiz = _mapper.Map<Quiz>(quizModel);
            return Ok(quiz);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Permissions.DeleteQuiz)]
        public IActionResult DeleteQuiz(int id)
        {
            _quizCoordinator.DeleteQuiz(id);
            return NoContent();
        }
    }
}
