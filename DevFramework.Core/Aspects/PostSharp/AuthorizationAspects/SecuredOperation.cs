using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.AuthorizationAspects
{
    [Serializable]
    public class SecuredOperation: OnMethodBoundaryAspect
    {
        public string Roles { get; set; }
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (System.Threading.Thread.CurrentPrincipal.Identity==null)
            {
                throw new SecurityException("Oturum Açılması Gerekli !");
            }
            string[] roles = Roles.Split(',');
            bool IsAuthorized = false;
            for (int i = 0; i < roles.Length; i++)
            {
                if (System.Threading.Thread.CurrentPrincipal.IsInRole(roles[i]))//gönderilen rollerden role sahipse (roller virgüllle gönderilecek)
                {
                    IsAuthorized = true;
                }
                
            }

            if(IsAuthorized==false)
            {
                throw new SecurityException("Bu İşlemi Yapmak İçin Yetkiniz Bulunmamakta !");
            }
            
        }


    }
}
