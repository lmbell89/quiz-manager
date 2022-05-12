using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizManager.Data.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public int Index { get; set; }
        public int QuizId { get; set; }
    }
}
