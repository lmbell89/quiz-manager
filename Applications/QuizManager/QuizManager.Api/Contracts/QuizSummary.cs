namespace QuizManager.Api.Contracts
{
    public class QuizSummary
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public QuestionSummary[] Questions { get; set; }
    }
}
