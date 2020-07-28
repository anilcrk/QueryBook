using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Entites.Concrete.Institutions
{
    public class Institution:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel01 { get; set; }
        public string Tel02 { get; set; }
        public string Tel03 { get; set; }
        public string Tel04 { get; set; }
        public string Fax01 { get; set; }
        public string Fax02 { get; set; }
        public string Fax03 { get; set; }
        public string Fax04 { get; set; }
        public string Address { get; set; }
        public string Explanation { get; set; }
        public string WebSite01 { get; set; }
        public string WebSite02 { get; set; }
        public string WebSite03 { get; set; }
        public string WebSite04 { get; set; }
        public string LogoPath { get; set; }
        public string BannerPath { get; set; }
    }
}
