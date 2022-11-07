using System.Data;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    //Validation Classlar İlgili Nesnenin Gerekli Doğrulamalara Sahipliğini Kontrol Eder.
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CompanyCode).NotEmpty().NotNull();
            RuleFor(c => c.CategoryName).NotEmpty().NotNull().MinimumLength(3);
        }
    }
}