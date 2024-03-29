﻿using NadinProductTask.Application.Commands.Base;
using NadinProductTask.Application.Validators.Product;
using NadinProductTask.Persist.Repository.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Commands.ProductCommands
{
	public class AddProductCommand : CommandBase
	{
		public string Name { get; set; } = string.Empty;

		public DateTime ProduceDate { get; set; }

		public string ManufacturePhone { get; set; } = string.Empty;

		public string ManufactureEmail { get; set; } = string.Empty;
		public bool IsAvailable { get; set; }

		[IgnoreDataMember]
		public string AthorUserName { get; set; } = string.Empty;



		/// <summary>
		/// اعتبار سنجی ورودی کاربر
		/// </summary>
		/// <returns>bool</returns>
		public override bool Validate()
		{
			return new AddProductCommandValidator().Validate(this).IsValid;
		}

		/// <summary>
		/// لیست ارور های ورودی های کاربر
		/// </summary>
		public dynamic ExecuteError()
		{
			return new AddProductCommandValidator().Validate(this).Errors;
		}
	}
}
