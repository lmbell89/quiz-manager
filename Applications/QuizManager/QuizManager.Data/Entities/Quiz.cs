using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizManager.Data.Entities
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
