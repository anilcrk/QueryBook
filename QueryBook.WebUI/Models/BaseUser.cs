using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryBook.WebUI.Models
{
    public class BaseUser
    {
        public int _UserId { get; set; }
        public string _InsCode { get; set; }
        public string _UserName { get; set; }
        public string _FirstName { get; set; }
        public string _LastName { get; set; }
    }
}