using System.ComponentModel.DataAnnotations;

namespace QuizManager.Data.Entities
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
