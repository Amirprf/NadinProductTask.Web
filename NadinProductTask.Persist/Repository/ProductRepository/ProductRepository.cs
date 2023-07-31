using Microsoft.EntityFrameworkCore;
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
				await _context.Products.AddAsync(product);
				await _context.SaveChangesAsync();
		}

		public async Task<List<Product>> GetAllAsync()
		{
			try
			{
				return await _context.Products.ToListAsync();
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task UpdateAsync(Product editProduct)
		{
			_context.Update(editProduct);
			await _context.SaveChangesAsync();
		}




		public async Task<Product> FindAsync(Guid id)
		{
			return await _context.Products.FindAsync(id); // if there is none => null
		}



		public async Task DeleteAsync(Product deleteProduct)
		{

			deleteProduct.DeleteProduct();
			_context.Update(deleteProduct);
			await _context.SaveChangesAsync();
		}






		#region Validations

		public async Task<bool> ManufactureEmailExistsAsync(string email)
		{
			return await _context.Products.AnyAsync(p => p.ManufactureEmail == email);
		}

		public async Task<bool> ProduceDateExistsAsync(DateTime dateTime)
		{
			return await _context.Products.AnyAsync(p => p.ProduceDate == dateTime);
		}
		#endregion
	}
}
