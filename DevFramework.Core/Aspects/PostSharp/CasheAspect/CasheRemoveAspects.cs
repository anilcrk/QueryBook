using DevFramework.Core.CrossCuttingConcerns.Cashing;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.CasheAspect
{
    [Serializable] // aspect olduğu için
  public  class CasheRemoveAspects:OnMethodBoundaryAspect //cashe silme operasyonları
    {
        private string _pattern;
        private Type _casheType;
        private ICashManager _casheManager;
        public CasheRemoveAspects(Type casheType) //info: CasheAspect.cs'ye bak
        {
            _casheType = casheType;
        }
        public CasheRemoveAspects(string pattern,Type casheType)
        {
            _pattern = pattern;
            _casheType = casheType;
        }


        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICashManager).IsAssignableFrom(_casheType) == false)//eğer cashetype cashe manager türünde değilse hata fıtlat
            {
                throw new Exception("Wrong Cashe Manager");
            }
            _casheManager = (ICashManager)Activator.CreateInstance(_casheType); //sınıf örneğini oluşturduk. Reflection
            base.RuntimeInitialize(method);
        }

        public override void OnSuccess(MethodExecutionArgs args)//method başarılı ise cashe silme işlemi
        {
            _casheManager.RemoveByPattern(string.IsNullOrEmpty(_pattern) ? string.Format("{0}.{1}.*",args.Method.ReflectedType.Name,args.Method.ReflectedType.Name)
                :_pattern);
        }

    }
}
