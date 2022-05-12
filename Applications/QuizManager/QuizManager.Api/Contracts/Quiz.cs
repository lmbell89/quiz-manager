namespace QuizManager.Api.Contracts
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public QuestionWithAnswers[] Questions { get; set; }
    }
}
