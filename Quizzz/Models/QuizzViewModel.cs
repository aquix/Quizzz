using System;
using System.Collections.Generic;

namespace Quizzz.Models
{
    [Serializable]
    public class QuizzViewModel
    {
        public string Author { get; set; }
        public string Category { get; set; }
        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}