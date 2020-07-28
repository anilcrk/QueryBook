using QueryBook.Entites.Concrete.Institutions;
using QueryBook.Entites.Concrete.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryBook.WebUI.Models.Home
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            institution = new Institution();
            queries = new List<Query>();
            query = new Query();
        }
        public  Institution institution { get; set; }
        public List<Query> queries { get; set; }
        public Query query { get; set; }
    }
}