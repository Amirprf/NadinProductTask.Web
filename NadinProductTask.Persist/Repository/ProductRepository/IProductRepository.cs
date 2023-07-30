using NadinProductTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Persist.Repository.ProductRepository
{
	public interface IProductRepository
	{
		Task AddAsync(Product product);
		Task<List<Product>> GetAllAsync();
		Task UpdateAsync(Product product);
		Task<Product> FindAsync(Guid id);

		Task<bool> ManufactureEmailExistsAsync(string email);
		Task<bool> ProduceDateExistsAsync(DateTime dateTime);
	}
}
