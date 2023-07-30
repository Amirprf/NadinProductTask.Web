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
		public Task<List<ProductDto>> GetAllProducts()
		{
			throw new NotImplementedException();
		}
	}
}
