using QueryBook.Business.Abstract;
using QueryBook.Business.DependencyResolved.Ninject;
using QueryBook.Entites.Concrete.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IInstutionService instutionService = IstanceFactory.GetIstance<IInstutionService>();
            try
            {
                Console.Write("Kurum Kodu: ");
                string code = Console.ReadLine();
                Console.Write("Adı: ");
                string name = Console.ReadLine();
                Console.Write("Tel: ");
                string tel = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Adres: ");
                string adres = Console.ReadLine();
                Console.Write("Web Site Adresi : ");
                string website = Console.ReadLine();
                Console.Write("Açıklama: ");
               string aciklama= Console.ReadLine();
                var result = instutionService.Add(new Institution { Code = code,Name=name,Tel01=tel,Email=email,Address=adres,WebSite01=website,Explanation=aciklama, UserId=2 });
                Console.WriteLine(result.sError);
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
       
    }
}
