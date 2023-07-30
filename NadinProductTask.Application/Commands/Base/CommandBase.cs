using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Commands.Base
{
	public abstract class CommandBase : ICommandBase
	{
		public abstract bool Validate();
	}
}
