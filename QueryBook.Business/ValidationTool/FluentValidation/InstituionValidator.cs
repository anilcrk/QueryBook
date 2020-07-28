using FluentValidation;
using QueryBook.Entites.Concrete.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.Business.ValidationTool.FluentValidation
{
  public  class InstituionValidator:AbstractValidator<Institution>
    {
        public InstituionValidator()
        {
            RuleFor(i => i.Code).NotEmpty().WithMessage("Kod Alanı Boş Bırakılamaz !");
            RuleFor(i => i.Name).NotEmpty().WithMessage("İsim Alanı Boş Bırakılamaz !");
        }
    }
}
