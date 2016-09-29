using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Quizzz.Models;
using Quizzz.Util;
using Quizzz.Util.Exceptions;
using System.Web.Http;

namespace Quizzz.Controllers
{
    [InvalidParameterExceptionFilter]
    public class QuizzController : ApiController
    {
        [HttpPost]
        [Route("api/generate")]
        public string GenerateQuizz([FromBody]NewQuizzViewModel newQuizzData) {
            if (newQuizzData == null)
            {
                throw new InvalidParameterException("Invalid post parameter");
            }

            var jsonResult = JsonConvert.SerializeObject(newQuizzData.Quizz, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });

            if (newQuizzData.OutputType == OutputType.Json) {
                return jsonResult;
            } else if (newQuizzData.OutputType == OutputType.Xml) {
                var xmlNode = JsonConvert.DeserializeXmlNode(jsonResult, "quizz");
                return xmlNode.InnerXml;
            } else
            {
                throw new InvalidParameterException("Output type not found");
            }
        }
    }
}