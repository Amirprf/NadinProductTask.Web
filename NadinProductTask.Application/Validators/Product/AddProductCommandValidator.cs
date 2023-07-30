using FluentValidation;
using NadinProductTask.Application.Commands.ProductCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Validators.Product
{
	public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
	{
		public AddProductCommandValidator()
		{
			RuleFor(p => p.Name).NotNull().WithMessage("لطفا نام محصول را وارد کنید.")
				.NotEmpty().WithMessage("نام محصول نمیتواند خالی باشد");

			RuleFor(p => p.ManufactureEmail).NotNull().WithMessage("لطفا ایمیل را وارد کنید.")
				.NotEmpty().WithMessage("ایمیل نمیتواند خالی باشد")
				.EmailAddress().WithMessage("لطفا ایمیل معتبر وارد کنید");

			RuleFor(p => p.ManufacturePhone).NotNull().WithMessage("لطفا شماره تلفن را وارد کنید.")
				.NotEmpty().WithMessage("شماره تلفن نمیتواند خالی باشد")
				.MinimumLength(3).WithMessage("شماره تلفن نمیتواند کمتر از 3 کاراکتر باشد")
				.MaximumLength(11).WithMessage("شماره تلفن نمیتواند بیشتر از 11 کاراکتر باشد");
		}
	}
}
