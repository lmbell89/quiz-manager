namespace QuizManager.Api.Contracts
{
    public class QuestionDetails
    {
        public int QuizId { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
        public Answer[] Answers { get; set; }
    }
}
