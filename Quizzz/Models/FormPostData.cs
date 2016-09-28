using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzz.Models
{
    public class FormPostData
    {
        public QuizzViewModel Quizz { get; set; }
        public OutputType OutputType { get; set; }
    }
}