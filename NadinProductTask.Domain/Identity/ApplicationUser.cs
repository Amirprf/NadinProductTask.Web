using Microsoft.AspNetCore.Identity;
using NadinProductTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Domain.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public ApplicationUser(string username) : base(username)
		{
		}

		private ApplicationUser()
		{ }

	}
}
