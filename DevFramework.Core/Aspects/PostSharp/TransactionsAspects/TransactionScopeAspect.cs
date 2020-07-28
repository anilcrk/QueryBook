using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DevFramework.Core.Aspects.PostSharp.TransactionsAspects
{
    [Serializable]//aspect olabilmesi için 
   public class TransactionScopeAspect:OnMethodBoundaryAspect //postsharp
    {
        private TransactionScopeOption _option;
        public TransactionScopeAspect()
        {

        }
        public TransactionScopeAspect(TransactionScopeOption option)
        {
            _option = option;
        }


        public override void OnEntry(MethodExecutionArgs args)//methoda girildiğinde
        {
            args.MethodExecutionTag = new TransactionScope(_option); //methodu execute adeceğimiz tag transactionscope olack
        }

        public override void OnSuccess(MethodExecutionArgs args)//method başarılı olduğunda
        {
            ((TransactionScope)args.MethodExecutionTag).Complete();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            ((Transaction)args.MethodExecutionTag).Dispose();
        }

    }
}
