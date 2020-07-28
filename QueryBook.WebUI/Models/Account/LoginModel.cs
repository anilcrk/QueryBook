using QueryBook.Entites.Concrete.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryBook.WebUI.Models.Account
{
    public class LoginModel
    {
        public User User { get; set; }
        public bool RememberMe { get; set; }
    }
}