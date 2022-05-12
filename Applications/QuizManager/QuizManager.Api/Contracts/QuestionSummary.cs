namespace QuizManager.Api.Contracts
{
    public class QuestionSummary
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
    }
}
