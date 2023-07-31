using NadinProductTask.Application.Commands.Base;
using NadinProductTask.Application.Validators.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Commands.ProductCommands
{
	public class DeleteProductCommand : CommandBase
	{
		public Guid Id { get; set; }

		[IgnoreDataMember]
		public string AthorUserName { get; set; } = string.Empty;

		/// <summary>
		/// اعتبار سنجی ورودی های کاربر
		/// </summary>
		public override bool Validate()
		{
			return new DeleteProductCommandValidator().Validate(this).IsValid;
		}

		/// <summary>
		/// لیست ارور های ورودی های کاربر
		/// </summary>
		public dynamic ExecuteError()
		{
			return new DeleteProductCommandValidator().Validate(this).Errors;
		}

	}
}
