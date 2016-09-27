using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzz.Models
{
    public class NewTestViewModel
    {
        public string Author { get; set; }
        public string Question { get; set; }
        public string Category { get; set; }
        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}