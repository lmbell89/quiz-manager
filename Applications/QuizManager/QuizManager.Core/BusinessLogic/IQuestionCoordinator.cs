using QuizManager.Core.Models;

namespace QuizManager.Core.BusinessLogic
{
    public interface IQuestionCoordinator
    {
        public Question GetQuestionById(int id);
        public Question CreateQuestion(Question question);
        public Question UpdateQuestion(int id, Question question);
        public void DeleteQuestion(int id);
    }
}
