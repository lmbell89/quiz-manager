namespace QuizManager.Core.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Question[] Questions { get; set; }
    }
}
