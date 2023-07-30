using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Domain.Entities
{
	[Index(nameof(Product.ProduceDate), IsUnique = true)]
	[Index(nameof(Product.ManufactureEmail), IsUnique = true)]
	public class Product
	{
		public Product(string name, DateTime produceDate,
			string manufacturePhone, string manufactureEmail,
			bool isAvailable, bool isDelete, string athorUserName)
		{
			Id = Guid.NewGuid();
			Name = name;
			ProduceDate = produceDate;
			ManufacturePhone = manufacturePhone;
			ManufactureEmail = manufactureEmail;
			IsAvailable = isAvailable;
			IsDelete = isDelete;
			AthorUserName = athorUserName;
		}
		

		[Key]
		public Guid Id { get; private set; }

		[Required]
		[MaxLength(400)]
		public string Name { get; private set; } = string.Empty;

		/// <summary>
		/// تاریخ معرفی
		/// </summary>
		public DateTime ProduceDate { get; private set; }

		/// <summary>
		/// تلفن تولید کننده
		/// </summary>
		[Required]
		[MaxLength(11)]
		public string ManufacturePhone { get; private set; } = string.Empty;

		/// <summary>
		/// ایمیل تولید کننده
		/// </summary>
		[Required]
		[MaxLength(320)]
		public string ManufactureEmail { get; private set; } = string.Empty;

		/// <summary>
		/// موجود است؟
		/// </summary>
		public bool IsAvailable { get; private set; }

		/// <summary>
		/// حذف
		/// </summary>
		public bool IsDelete { get; private set; }

		/// <summary>
		/// محصول توسط این کاربر اضافه شده
		/// </summary>
		[Required]
		[MaxLength(450)]
		public string AthorUserName { get; private set; }

		public Product()
		{
		}

	}
}
