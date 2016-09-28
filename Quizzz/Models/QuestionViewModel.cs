using System;
using System.Collections.Generic;

namespace Quizzz.Models
{
    [Serializable]
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string QuestionBody { get; set; }
        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}