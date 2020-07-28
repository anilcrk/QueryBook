using QueryBook.Entites.Concrete.Querys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.DataAccess.Concrete.EntityFramework.Mappings
{
   public class QueryMap:EntityTypeConfiguration<Query>
    {
        public QueryMap()
        {
            ToTable(@"Querys", @"dbo");
            HasKey(x => x.Id);

            Property(x => x.InsCode).HasColumnName("InsCode");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.QueryTypeId).HasColumnName("QueryTypeId");
            Property(x => x.ProjectId).HasColumnName("ProjectId");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.OrderNo).HasColumnName("OrderNo");
            Property(x => x.Active).HasColumnName("Active");
            Property(x => x.Explanation).HasColumnName("Explanation");
            Property(x => x.QueryScript).HasColumnName("QueryScript");
            Property(x => x.ImagePath).HasColumnName("ImagePath");
            Property(x => x.IsDel).HasColumnName("IsDel");
            Property(x => x.InsUserId).HasColumnName("InsUserId");
            Property(x => x.EditUserId).HasColumnName("EditUserId");
            Property(x => x.EditDate).HasColumnName("EditDate");
            Property(x => x.DelUserId).HasColumnName("DelUserId");
            Property(x => x.DelDate).HasColumnName("DelDate");
        }
    }
}
