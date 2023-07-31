using NadinProductTask.Application.Commands.Base;
using NadinProductTask.Application.Validators.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Commands.IdentityCommand
{
	public class RegisterCommand : CommandBase
	{

		public string? Username { get; set; }

		public string? Email { get; set; }


		public string? Password { get; set; }


		/// <summary>
		/// اعتبار سنجی ورودی کاربر
		/// </summary>
		/// <returns>bool</returns>
		public override bool Validate()
		{
			return new RegisterCommandValidator().Validate(this).IsValid;
		}

		/// <summary>
		/// لیست ارور های ورودی های کاربر
		/// </summary>
		public dynamic ExecuteError()
		{
			return new RegisterCommandValidator().Validate(this).Errors;
		}
	}
}
