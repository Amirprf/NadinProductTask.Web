using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Dtos
{
	public class ProductDto
	{
		public Guid Id { get; set; }
		/// <summary>
		/// نام محصول
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// تاریخ معرفی
		/// </summary>
		public DateTime ProduceDate { get; set; }

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

		/// <summary>
		/// کاربر ایجاد کننده
		/// </summary>
		public string AthorUserName { get; set; } = string.Empty;
	}
}
