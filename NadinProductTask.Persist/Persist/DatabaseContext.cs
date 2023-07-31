using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NadinProductTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Persist.Persist
{
	public class DatabaseContext : IdentityDbContext<IdentityUser>
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

		#region DBSet Properties

		public DbSet<Product> Products { get; set; }

		#endregion

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Product>()
				.HasQueryFilter(u => !u.IsDelete);
		}
	}
}
