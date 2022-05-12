using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QuizManager.Api.Auth;
using QuizManager.Api.Contracts;
using QuizManager.Core.BusinessLogic;

namespace QuizManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IQuestionCoordinator _questionCoordinator;

        public QuestionController(IMapper mapper, IQuestionCoordinator questionCoordinator)
        {
            _mapper = mapper;
            _questionCoordinator = questionCoordinator;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Permissions.ReadQuestion)]
        [Authorize(Permissions.ReadAnswer)]
        public IActionResult GetQuestionById(int id)
        {
            var questionModel = _questionCoordinator.GetQuestionById(id);
            var question = _mapper.Map<QuestionWithAnswers>(questionModel);
            return Ok(question);
        }

        [HttpPost]
        [Authorize(Permissions.ReadQuestion)]
        [Authorize(Permissions.CreateQuestion)]
        public IActionResult CreateQuestion([FromBody] QuestionDetails question)
        {
            var questionModel = _mapper.Map<Core.Models.Question>(question);
            var createdModel = _questionCoordinator.CreateQuestion(questionModel);
            var createdQuestion = _mapper.Map<QuestionWithAnswers>(createdModel);
            return Ok(createdQuestion);
        }

        [HttpPatch]
        [Route("{id}")]
        [Authorize(Permissions.ReadQuestion)]
        [Authorize(Permissions.UpdateQuestion)]
        public IActionResult UpdateQuestion(int id, [FromBody] QuestionDetails question)
        {
            var questionModel = _mapper.Map<Core.Models.Question>(question);
            var updatedModel = _questionCoordinator.UpdateQuestion(id, questionModel);
            var updatedQuestion = _mapper.Map<QuestionWithAnswers>(updatedModel);
            return Ok(updatedQuestion);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Permissions.DeleteQuestion)]
        public IActionResult DeleteQuestion(int id)
        {
            _questionCoordinator.DeleteQuestion(id);
            return NoContent();
        }
    }
}
