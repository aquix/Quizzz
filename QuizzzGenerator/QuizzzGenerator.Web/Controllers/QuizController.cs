using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Quizzz.Models;
using Quizzz.Util;
using Quizzz.Util.Exceptions;

namespace Quizzz.Controllers
{
    [InvalidParameterExceptionFilter]
    public class QuizController : Controller
    {
        [HttpPost]
        [Route("api/generate")]
        public string GenerateQuiz([FromBody]NewQuizViewModel newQuizData) {
            if (newQuizData == null)
            {
                throw new InvalidParameterException("Invalid post parameter");
            }

            var jsonResult = JsonConvert.SerializeObject(newQuizData.Quiz, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });

            if (newQuizData.OutputType == OutputType.Json) {
                return jsonResult;
            } else if (newQuizData.OutputType == OutputType.Xml) {
                var xmlNode = JsonConvert.DeserializeXNode(jsonResult, "quizz");
                return xmlNode.ToString();
            } else
            {
                throw new InvalidParameterException("Output type not found");
            }
        }
    }
}