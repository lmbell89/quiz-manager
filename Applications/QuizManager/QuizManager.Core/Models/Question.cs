namespace QuizManager.Core.Models
{
    public class Question
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public Answer[] Answers { get; set; }
        public int Index { get; set; }
        public int QuizId { get; set; }
    }
}
