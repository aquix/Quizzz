using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Web.Models.ApiViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string QuestionBody { get; set; }
        public IEnumerable<string> Answers { get; set; }
    }
}
