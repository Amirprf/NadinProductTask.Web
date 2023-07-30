using NadinProductTask.Application.Commands.Base;
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
			return true;
		}

		/// <summary>
		/// لیست ارور های ورودی های کاربر
		/// </summary>
		public dynamic ExecuteError()
		{
			return true;
		}
	}
}
