using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Models.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductModelValidator : AbstractValidator<CreateProductModel>
    {
        public CreateProductModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Product name cannot be null!")
                .MaximumLength(150)
                .MinimumLength(2)
                    .WithMessage("Product name leng must be between 2-150!");

            RuleFor(p => p.Stock)
                .NotNull()
                    .WithMessage("Product stock cannot be null!")
                .Must(p => p >= 0)
                .WithMessage("Product stock must greater or equals zero");

            RuleFor(p => p.Price)
                .NotNull()
                    .WithMessage("Product price cannot be null!")
                .Must(p => p >= 0)
                .WithMessage("Product price must greater or equals zero");
        }
    }
}
