using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Entities
{
    public class QuizBestResult
    {
        public string QuizId { get; set; }
        public string Name { get; set; }
        public int QuestionsCount { get; set; }
        public int PassedQuestionsCount { get; set; }
        public bool IsPassed { get; set; }
    }
}
