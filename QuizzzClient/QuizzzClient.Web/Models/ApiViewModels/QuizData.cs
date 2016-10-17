using QuizzzClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Web.Models.ApiViewModels
{
    public class QuizData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Time { get; set; }
        public string Category { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
