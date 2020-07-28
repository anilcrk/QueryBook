using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Entites.Concrete
{
     public  class BaseEntity:IEntity
    {
        public string InsCode { get; set; }
        public byte IsDel { get; set; }
        public int InsUserId { get; set; }
        public DateTime InsDate { get; set; }
        public System.Nullable<int> EditUserId { get; set; }
        public System.Nullable<DateTime> EditDate { get; set; }
        public System.Nullable<int> DelUserId { get; set; }
        public System.Nullable<DateTime> DelDate { get; set; }
        [NotMapped]
        public int UserId { get; set; }
        [NotMapped]
        public string sError { get; set; }
    }
}
