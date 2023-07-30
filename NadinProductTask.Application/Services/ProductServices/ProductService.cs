using NadinProductTask.Application.Commands.ProductCommands;
using NadinProductTask.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Services.ProductServices
{
	public class ProductService : IProductService
	{
		public async Task AddProductAsync(AddProductCommand command)
		{
			return;
		}

		public async Task<List<ProductDto>> GetAllProducts()
		{
			throw new NotImplementedException();
		}
	}
}
