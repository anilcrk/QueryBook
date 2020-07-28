using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.PerformanceAspects
{
    [Serializable]
    public class PerformanceCounterAspect:OnMethodBoundaryAspect
    {
        private int _interval;
        [NonSerialized]//serialize edilmeyecek
        private Stopwatch/*Kronometre için*/ _stopwatch;

        public PerformanceCounterAspect(int interval=5)
        {
            _interval = interval;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            _stopwatch = Activator.CreateInstance<Stopwatch>(); //stopwatchın tipini alma
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            _stopwatch.Restart();
            _stopwatch.Start();
            base.OnEntry(args);
        }
        public override void OnExit(MethodExecutionArgs args)
        {
            _stopwatch.Stop();
            if(_stopwatch.Elapsed.TotalSeconds>_interval)
            {
                Debug.WriteLine("Performance EROOR: {0}.{1}-->{2}", args.Method.DeclaringType.FullName, args.Method.Name, _stopwatch.Elapsed.TotalSeconds);
            }
            Debug.WriteLine("Performance NORMAL: {0}.{1}-->{2}", args.Method.DeclaringType.FullName, args.Method.Name, _stopwatch.Elapsed.TotalSeconds);
            base.OnExit(args);
        }
    }
}
/*
 --------------------NOTLAR-------------------------
 -> args.Method.DeclaringType.FullName ->Methodun namespacesini alır
 -> args.Method.Name -> Methodun adını alır.
 
     
     */
