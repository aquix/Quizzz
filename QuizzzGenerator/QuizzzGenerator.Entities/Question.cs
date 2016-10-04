using System.Collections.Generic;

namespace Quizzz.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionBody { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}