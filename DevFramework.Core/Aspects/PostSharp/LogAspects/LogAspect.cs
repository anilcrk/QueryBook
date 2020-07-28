using DevFramework.Core.CrossCuttingConcerns.Logging;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.LogAspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)] //sadece methodlara uygulanabilir klasın tepesine konursa
    public class LogAspect : OnMethodBoundaryAspect//Aspect olduğu için
    {
        private Type _loggerType;
        private LoggerService _loggerService;
        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        public override void RuntimeInitialize(MethodBase method) //istance üretmek için onmethodBoundreaspectten geliyor
        {
            if (_loggerType.BaseType != typeof(LoggerService)) //eğer gelen rip loggerserviceden implemente edilmediyse hata fırlat
            {
                throw new Exception("Wrong Logger Type");
            }
            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType); //gelen tipin istancesini üret
            base.RuntimeInitialize(method);
        }


        public override void OnEntry(MethodExecutionArgs args) //methoda girildiğinde
        {
            if(!_loggerService.IsInfoEnabled)
            {
                return;
            }
            try
            {
                var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter {
                    Name = t.Name,  //parametre adı
                    Type = t.ParameterType.Name, //parametre tipi string int vs
                    Value = args.Arguments.GetArgument(i) //parametre değeri
                }).ToList();

                var logDetail = new LogDetail
                {
                    FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                    MethodName = args.Method.Name,
                    Parameters = logParameters
                };

                _loggerService.info(logDetail); //info seviyesinde log oluşturuldu
            }
            catch(Exception)
            {

            }
        }
    }
}
