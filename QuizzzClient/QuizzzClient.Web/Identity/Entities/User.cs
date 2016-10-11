using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.MongoDB;
using MongoDB.Bson;
using QuizzzClient.Entities;

namespace QuizzzClient.Web.Identity.Entities
{
    public class User : IdentityUser
    {
        public IList<QuizBestResult> BestResults { get; set; }
    }
}
