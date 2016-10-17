using System.Collections.Generic;

namespace Quizzz.Entities
{
    public class Quiz
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Time { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}