using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Quizzz.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Quizzz.Controllers
{
    public class QuizzController : Controller
    {
        [Route("generate")]
        public ActionResult GenerateQuizz(string jsonData) {
            var postData = JsonConvert.DeserializeObject<FormPostData>(jsonData, new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            if (postData.OutputType == "json") {
                var jsonResult = JsonConvert.SerializeObject(postData.Quizz, new JsonSerializerSettings {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                });
                return Content(jsonResult, "application/json");
            } else if (postData.OutputType == "xml") {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(NewQuizzViewModel));
                using (StringWriter textWriter = new StringWriter()) {
                    xmlSerializer.Serialize(textWriter, postData.Quizz);
                    return Content(textWriter.ToString(), "text/xml");
                }
            } else {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}