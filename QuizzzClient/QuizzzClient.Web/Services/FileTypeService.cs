using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Web.Services
{
    public static class FileTypeService
    {
        public static bool IsJson(string content) {
            if (content.First() == '{') {
                return true;
            }

            return false;
        }

        public static bool IsXml(string content) {
            if (content.First() == '<') {
                return true;
            }

            return false;
        }
    }
}
