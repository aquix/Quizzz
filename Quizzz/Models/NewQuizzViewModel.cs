using System;
using System.Collections.Generic;

namespace Quizzz.Models
{
    [Serializable]
    public class NewQuizzViewModel
    {
        public string Author { get; set; }
        public string Question { get; set; }
        public string Category { get; set; }
        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}