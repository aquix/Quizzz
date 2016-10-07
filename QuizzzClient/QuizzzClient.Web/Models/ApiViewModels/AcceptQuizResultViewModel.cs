using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Web.Models.ApiViewModels
{
    public class AcceptQuizResultViewModel
    {
        public string Id { get; set; }
        public int AllQuestionsCount { get; set; }
        public int PassedQuestionsCount { get; set; }
        public bool Success { get; set; }
    }
}
