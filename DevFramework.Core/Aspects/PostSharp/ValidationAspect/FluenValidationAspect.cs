using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using FluentValidation;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.ValidationAspect
{
    [Serializable] //Aspect işlemi için eklenmesi gerek
    public class FluenValidationAspect:OnMethodBoundaryAspect
    {
        Type _validatorType;
        public FluenValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }
        public override void OnEntry(MethodExecutionArgs args/*çalışan methodla ilgili bilgi verir*/)//methoda girdiğinde
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//Gelen validaton clasının tipini(insancesini) almak
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//gelen classın generic nesnesini yakalar. ProductValidatior içinde sadece bir tane olduğu için 0 indisli aldık.
            var entites = args.Arguments.Where(t => t.GetType() == entityType);//çalışan methodtaki parametreleri yakalar dizi olarak atar

            foreach (var entity in entites)
            {
                ValidatorTool.FluentValidate(validator, entity); //gelen class ve entity core katmanında yazdığımız validatortool içindeki methoda gönderdik
            }
        }
    }
}
