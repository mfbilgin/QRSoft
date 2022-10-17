using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.CategoryId).NotEmpty().NotNull();
            RuleFor(p => p.CompanyCode).NotEmpty().NotNull();
            RuleFor(p => p.ProductName).NotEmpty().NotNull();
            RuleFor(p => p.UnitPrice).NotEmpty().NotNull();
        }
    }
}