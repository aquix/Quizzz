using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizzzClient.Web.Models.ApiViewModels
{
    public class AcceptQuizViewModel
    {
        public string QuizId { get; set; }
        public int TakenTime { get; set; }
        public IEnumerable<IEnumerable<int>> Answers { get; set; }
    }
}
