using NadinProductTask.Application.Commands.Base;
using NadinProductTask.Application.Validators.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Commands.ProductCommands
{
	public class EditProductCommand : CommandBase
	{

		public Guid Id { get; set; }

		/// <summary>
		/// نام محصول
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// تاریخ معرفی
		/// </summary>
		public DateTime ProduceDate { get; private set; }

		/// <summary>
		/// تلفن تولید کننده
		/// </summary>
		public string ManufacturePhone { get; set; } = string.Empty;

		/// <summary>
		/// ایمیل تولید کننده
		/// </summary>
		public string ManufactureEmail { get; set; } = string.Empty;

		/// <summary>
		/// موجود است؟
		/// </summary>
		public bool IsAvailable { get; set; }

		/// <summary>
		/// حذف
		/// </summary>
		public bool IsDelete { get; set; }

		public string AthorUserName { get; set; } = string.Empty;

		/// <summary>
		/// اعتبار سنجی ورودی های کاربر
		/// </summary>
		public override bool Validate()
		{
			return new EditProductCommandValidator().Validate(this).IsValid;
		}

		/// <summary>
		/// لیست ارور های ورودی های کاربر
		/// </summary>
		public dynamic ExecuteError()
		{
			return new EditProductCommandValidator().Validate(this).Errors;
		}
	}
}
