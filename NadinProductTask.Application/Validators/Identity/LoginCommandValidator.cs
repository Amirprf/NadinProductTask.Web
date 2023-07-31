using FluentValidation;
using NadinProductTask.Application.Commands.IdentityCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Validators.Identity
{
	public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(p => p.Username).NotNull().WithMessage("لطفا نام کاربری خود را وارد کنید.")
				.NotEmpty().WithMessage("نام کاربری نمیتواند خالی باشد");

			RuleFor(p => p.Password).NotNull().WithMessage("لطفا رمز عبور خود را وارد کنید.")
				.NotEmpty().WithMessage("رمز عبور نمیتواند خالی باشد");
		}
	}
}
