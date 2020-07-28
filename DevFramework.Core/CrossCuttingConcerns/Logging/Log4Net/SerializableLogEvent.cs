using DevFramework.Core.CrossCuttingConcerns.Security;
using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net
{
    [Serializable]
  public  class SerializableLogEvent
    {
        private LoggingEvent _loggingEvent;
       
        public SerializableLogEvent(LoggingEvent loggingEvent) //log4net
        {
            _loggingEvent = loggingEvent;
        }

        public string UserNAme => _loggingEvent.UserName; //k.adi
        public object MessageObject => _loggingEvent.MessageObject; //mesaj
    }
}
