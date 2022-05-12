using System;
using System.Collections.Generic;
using QuizManager.Core.Models;

namespace QuizManager.Core.BusinessLogic
{
    public interface IQuizCoordinator
    {
        public IEnumerable<Quiz> GetQuizzes(
            int pageSize, 
            int pageIndex, 
            string matchTitle = null);
        public Quiz GetQuizById(int id);
        public Quiz CreateQuiz(string title);
        public Quiz UpdateTitle(int id, string title);
        public void DeleteQuiz(int id);
    }
}
