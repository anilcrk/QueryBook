using QueryBook.DataAccess.Concrete.EntityFramework.Mappings;
using QueryBook.Entites.Concrete.Querys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.DataAccess.Concrete.EntityFramework
{
   public class QueryBookContext:DbContext
    {
        public QueryBookContext()
        {
            Database.SetInitializer<QueryBookContext>(null);
        }
        public DbSet<Query> Querys { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new QueryMap());
        }
    }
}
