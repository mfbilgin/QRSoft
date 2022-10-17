using Core.Entities.Concrete;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.MailAddress).EmailAddress().NotEmpty().NotNull();
            RuleFor(c => c.PhoneNumber).Length(10).NotEmpty().NotNull();
            RuleFor(c => c.CompanyName).NotEmpty().NotNull();
        }
    }
}