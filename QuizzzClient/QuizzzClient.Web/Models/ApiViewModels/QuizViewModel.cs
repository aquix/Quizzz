using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Web.Models.ApiViewModels
{
    public class QuizViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int Time { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
