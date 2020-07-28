using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Entites.Concrete.Users
{
    public class User:BaseEntity
    {
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public string ImagePath { get; set; }
        public string UserGroupName { get; set; }
    }
}
