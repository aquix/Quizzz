using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using QuizzzClient.DataAccess.Interfaces;
using Newtonsoft.Json;
using QuizzzClient.Entities;
using System.Linq;
using System.Xml.Linq;

namespace QuizzzClient.Web.Controllers.Api
{
    [Route("api/quizz")]
    public class QuizzController : Controller
    {
        private IUnitOfWork db;

        public QuizzController(IUnitOfWork db) {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file) {
            string fileContent;
            Quizz quizz;

            using (var stream = file.OpenReadStream()) {
                var streamReader = new StreamReader(stream);
                fileContent = await streamReader.ReadToEndAsync();
                streamReader.Dispose();
            }

            if (IsJson(fileContent)) {
                try {
                    quizz = JsonConvert.DeserializeObject<Quizz>(fileContent);
                    db.Quizzes.Add(quizz);
                } catch (JsonException) {
                    return StatusCode(500);
                }
                
            } else if (IsXml(fileContent)) {
                try {
                    var doc = new XDocument(fileContent);
                    string jsonText = JsonConvert.SerializeXNode(doc);
                    quizz = JsonConvert.DeserializeObject<Quizz>(fileContent);
                    db.Quizzes.Add(quizz);
                } catch (JsonException) {
                    return StatusCode(500);
                }
            } else {
                return StatusCode(500);
            }

            Debug.Write("created");
            return Created("api/quizz", 0);
        }

        private bool IsJson(string content) {
            if (content.First() == '{') {
                return true;
            }

            return false;
        }

        private bool IsXml(string content) {
            if (content.First() == '<') {
                return true;
            }

            return false;
        }
    }
}