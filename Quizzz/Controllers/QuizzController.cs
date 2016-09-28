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

            var jsonResult = JsonConvert.SerializeObject(postData.Quizz, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });

            if (postData.OutputType == "json") {
                return Content(jsonResult, "application/json");
            } else if (postData.OutputType == "xml") {
                var xmlNode = JsonConvert.DeserializeXmlNode(jsonResult, "quizz");
                return Content(xmlNode.InnerXml, "text/xml");
            } else {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        private bool IsCorrect(QuizzViewModel quizz)
        {
            return true;
        }
    }
}