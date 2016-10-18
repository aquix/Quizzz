using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Web.Models.ApiViewModels
{
    public class AllPreviewsViewModel
    {
        public IEnumerable<QuizPreviewViewModel> Quizzes { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public int TotalPages { get; set; }
    }
}
