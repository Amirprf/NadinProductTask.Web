using NadinProductTask.Domain.Entities;
using NadinProductTask.Persist.Persist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Persist.Repository.ProductRepository
{
	public class ProductRepository : IProductRepository
	{
		protected readonly DatabaseContext _context;

		public ProductRepository(DatabaseContext context)
		{
			_context = context;

		}
		public async Task AddAsync(Product product)
		{
			throw new NotImplementedException();
		}
	}
}
