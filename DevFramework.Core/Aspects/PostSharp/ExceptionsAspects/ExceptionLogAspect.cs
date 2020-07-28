using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.ExceptionsAspects
{
    [Serializable]
  public  class ExceptionLogAspect:OnExceptionAspect//postshrap
    {
        [NonSerialized]
        private LoggerService _loggerService;
        private readonly Type _loogerType;
        public ExceptionLogAspect(Type loggerType=null)
        {
            _loogerType = loggerType;

        }
        public override void RuntimeInitialize(MethodBase method)
        {
            if(_loogerType!=null)
            {
                if (_loogerType.BaseType != typeof(LoggerService))
                    throw new Exception("Wrong Logger Type");
                _loggerService = (LoggerService)Activator.CreateInstance(_loogerType);
            }
            base.RuntimeInitialize(method);
        }
        public override void OnException(MethodExecutionArgs args)
        {
            if(_loggerService!=null)
            {
                _loggerService.Error(args.Exception);
            }
        }
    }
}
