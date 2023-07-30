using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Commands.Base
{
	public interface ICommandBase
	{
		/// <summary>
		/// اعتبارسنجی کامند های ورودی کاربر
		/// </summary>
		bool Validate();
	}
}
