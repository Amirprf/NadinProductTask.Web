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
			Product product = FindAsync(editProduct.Id).Result;
			
			if (product.AthorUserName != editProduct.AthorUserName)
			{
				throw new AccessViolationException("شما اجازه ویرایش این محصول را ندارید", null);
			}
			if (_context.Products.Any(p => p.ManufactureEmail == editProduct.ManufactureEmail || p.ProduceDate == editProduct.ProduceDate))
			{
				throw new Exception("محصول تکراری میباشد",null);
			}

			_context.Update(product);
			await _context.SaveChangesAsync();
		}




		public async Task<Product> FindAsync(Guid id)
		{
			return await _context.Products.FindAsync(id); // if there is none => null
		}


	}
}
