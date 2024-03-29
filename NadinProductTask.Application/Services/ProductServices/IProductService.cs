﻿using NadinProductTask.Application.Commands.ProductCommands;
using NadinProductTask.Application.Dtos;
using NadinProductTask.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Services.ProductServices
{
	public interface IProductService
	{
		Task<List<ProductDto>> GetAllProducts(GetAllProductQuery query);
		Task AddProductAsync(AddProductCommand command);
		Task UpdateProductById(EditProductCommand command);
		Task<int> DeleteProduct(DeleteProductCommand command);
		Task<ProductDto> GetProductById(Guid id);


		Task<bool> IsUniqueEmail(string email);
		Task<bool> IsUniqueProduceDate(DateTime time);
	}
}
