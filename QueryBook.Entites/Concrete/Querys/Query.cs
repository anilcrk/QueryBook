using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Entites.Concrete.Querys
{
   public  class Query:BaseEntity, IEntity
    {
       // public string InsCode { get; set; }
        public int Id { get; set; }
        public int QueryTypeId { get; set; }
        public int ProjectId { get; set; }
        [DisplayName("Script Adı")]
        public string Name { get; set; }
        [DisplayName("Sıra No")]
        public int OrderNo { get; set; }
        [DisplayName("Aktif mi?")]
        public byte Active { get; set; }
        [DisplayName("Açıklama")]
        public string Explanation { get; set; }
        [DisplayName("Script")]
        public string QueryScript { get; set; }
        public string ImagePath { get; set; }
    }
}
