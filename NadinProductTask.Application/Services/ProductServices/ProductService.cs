using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NadinProductTask.Application.Commands.ProductCommands;
using NadinProductTask.Application.Dtos;
using NadinProductTask.Domain.Entities;
using NadinProductTask.Persist.Repository.ProductRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Services.ProductServices
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public ProductService(IProductRepository productRepository,IMapper mapper)
        {
			_productRepository = productRepository;
			_mapper = mapper;
		}

        public async Task AddProductAsync(AddProductCommand command)
		{
			var product = _mapper.Map<Product>(command);

			bool isExistEmail = IsUniqueEmail(product.ManufactureEmail).Result;
			bool isExistDate = IsUniqueProduceDate(product.ProduceDate).Result;

			if(isExistEmail || isExistDate)
			{
				throw new InvalidOperationException();
			}
			try
			{
				await _productRepository.AddAsync(product);
			}
			catch (Exception)
			{
				return;
			}
		}

		public async Task<List<ProductDto>> GetAllProducts()
		{

			 var products= await _productRepository.GetAllAsync();

			List<ProductDto> productsDto= _mapper.Map<List<ProductDto>>(products);

			return productsDto;
		}


		public async Task UpdateProductById(EditProductCommand command)
		{
			var editProduct = _mapper.Map<Product>(command);

			Product product = _productRepository.FindAsync(editProduct.Id).Result;

			bool isExistEmail = IsUniqueEmail(editProduct.ManufactureEmail).Result;
			bool isExistDate = IsUniqueProduceDate(editProduct.ProduceDate).Result;

			if (product.AthorUserName != editProduct.AthorUserName)
			{
				throw new AccessViolationException();
			}
			if (isExistEmail || isExistDate)
			{
				throw new InvalidOperationException();
			}
			try
			{
				await _productRepository.UpdateAsync(editProduct);
			}
			catch (Exception)
			{

				return;
			}
		}

		public async Task<int> DeleteProduct(DeleteProductCommand command)
		{
			var product =_productRepository.FindAsync(command.Id).Result;

			if (product != null)
			{
				if (product.AthorUserName != command.AthorUserName)
				{
					return 2;
				}
				await _productRepository.DeleteAsync(product);

				return 1;
			}
			return 0;
		}

		public async Task<ProductDto> GetProductById(Guid id)
		{
			var product = await _productRepository.FindAsync(id);

			return _mapper.Map<ProductDto>(product);
		}





		public async Task<bool> IsUniqueEmail(string email)
		{
			var isExsitsEmail = await _productRepository.ManufactureEmailExistsAsync(email);

			return isExsitsEmail;
		}

		public async Task<bool> IsUniqueProduceDate(DateTime time)
		{
			var isExsitsDate = await _productRepository.ProduceDateExistsAsync(time);

			return isExsitsDate;
		}

		
	}
}
