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
    [Serializable] //aspect olduğu için
  public  class CasheAspect:MethodInterceptionAspect//postsharp
    {
        private Type _casheType; //hangi cashe tipi örn: memorycase,rediscashe
        private int _casheMinute; //cash'de nekadar tutulacağı
        private ICashManager _casheManager; //hangi cashe mekanizmasını kullanacaksa 

        public CasheAspect(Type casheType,int casheMinute=60)//defalut 60 dk
        {
            _casheType = casheType;
            _casheMinute = casheMinute;
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


        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0},{1},{2}",
                args.Method.ReflectedType.Namespace, args.Method.ReflectedType.Name, args.Method.Name); //methodname oluşturma :methodnamespca,method class, method name
            var arguments = args.Arguments.ToList();//method parametrelerine ulaşma
            
            var key = string.Format("{0}({1})",methodName, string.Join(",",arguments.Select(x=>x!=null?x.ToString():"<Null>")));//cashe key oluşturma

            if(_casheManager.IsAdd(key))//method cashede varmı ?
            {
                args.ReturnValue = _casheManager.Get<object>(key); //varsa devam etme git cashemanagerdan cashi getir key'e göre
            }
            base.OnInvoke(args);
            _casheManager.Add(key, args.ReturnValue, _casheMinute);//cashde yoksa ekle
        }
    }
}
