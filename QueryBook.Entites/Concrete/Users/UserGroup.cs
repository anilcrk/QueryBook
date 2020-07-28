using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Entites.Concrete.Users
{
   public  class UserGroup:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
