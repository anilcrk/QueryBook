using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net
{
    [Serializable]
    public class LoggerService
    {
        private ILog _log; //Log4net den geliyor ILog
        public LoggerService(ILog log)
        {
            _log = log;
        }

        public bool IsInfoEnabled => _log.IsInfoEnabled;//info logları açıkmı ? 
        public bool IsDebugEnabled => _log.IsDebugEnabled;//debug logları açıkmı ?
        public bool IsWarnEnabled => _log.IsWarnEnabled;//warn logları açıkmı ?
        public bool IsFatalEnabled => _log.IsFatalEnabled;//fatal logları açıkmı ?
        public bool IsErrorEnabled => _log.IsFatalEnabled;//fatal logları açıkmı ?

        public void info(object logMessage)
        {
            if(IsInfoEnabled)
            {
                _log.Info(logMessage); //info seviyesinde örn: şu kişi şu metodu şu tarihte şu kadar kullanıdı
            }
        }
        public void Debug(object logMessage)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(logMessage);
            }
        }
        public void Warn(object logMessage)
        {
            if (IsWarnEnabled )
            {
                _log.Info(logMessage);
            }
        }
        public void Fatal(object logMessage)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(logMessage);
            }
        }

        public void Error(object logMessage)
        {
            if (IsErrorEnabled)
            {
                _log.Error(logMessage);
            }
        }
    }
}
