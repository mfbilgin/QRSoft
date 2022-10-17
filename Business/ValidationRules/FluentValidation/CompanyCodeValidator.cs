using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CompanyCodeValidator : AbstractValidator<CompanyCode>
    {
        public CompanyCodeValidator()
        {
            RuleFor(code => code.CompanyId).NotEmpty().NotNull();
        }
    }
}