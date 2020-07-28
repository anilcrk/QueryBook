using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Entites.Concrete.Projects
{
  public class Project:BaseEntity
    {
        public string InsCode { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNo { get; set; }
        public string Explanation { get; set; }
        public string ImagePath { get; set; }
    }
}
