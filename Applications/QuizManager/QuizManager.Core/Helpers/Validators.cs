using System.Linq;
using QuizManager.Core.Exceptions;
using QuizManager.Core.Models;

namespace QuizManager.Core.Helpers
{
    internal static class Validators
    {
        internal static void ValidateAnswer(Answer answer)
        {
            if (string.IsNullOrWhiteSpace(answer.Text))
            {
                throw new InvalidAnswerException("Text cannot be empty");
            }
        }

        internal static void ValidateQuestion(Question question)
        {
            if (question.Answers == null || question.Answers.Length < 3)
            {
                throw new InvalidQuestionException("Question must have at least 3 answers");
            }
            if (question.Answers.Length > 5)
            {
                throw new InvalidQuestionException("Question cannot have more than 5 answers");
            }
            if (!question.Answers.Any(a => a.IsCorrect))
            {
                throw new InvalidQuestionException($"At least one answer must be correct");
            }
            if (!question.Answers.Any(a => !a.IsCorrect))
            {
                throw new InvalidQuestionException("At least one answer must be incorrect");
            }
            if (string.IsNullOrWhiteSpace(question.Text))
            {
                throw new InvalidQuestionException("Question text cannot be blank");
            }
            if (question.Index < 0)
            {
                throw new InvalidQuestionException("Question index cannot be negative");
            }
        }

        internal static void ValidateQuiz(Quiz quiz)
        {
            if (string.IsNullOrWhiteSpace(quiz.Title))
            {
                throw new InvalidQuizException("Title cannot be empty");
            }
        }
    }
}
