using System.Collections.Generic;

namespace Quizzz.Entities
{
    public class Quizz
    {
        public string Author { get; set; }
        public string Category { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}