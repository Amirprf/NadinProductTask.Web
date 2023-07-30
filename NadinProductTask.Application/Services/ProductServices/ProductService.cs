﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NadinProductTask.Application.Commands.ProductCommands;
using NadinProductTask.Application.Dtos;
using NadinProductTask.Domain.Entities;
using NadinProductTask.Persist.Repository.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
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
			
			await _productRepository.AddAsync(product);
		}

		public async Task<List<ProductDto>> GetAllProducts()
		{

			 var products= await _productRepository.GetAllAsync();

			return _mapper.Map<List<ProductDto>>(products);
		}

		public async Task UpdateProductById(EditProductCommand command)
		{
			var editProduct = _mapper.Map<Product>(command);
			await _productRepository.UpdateAsync(editProduct);
		}
	}
}
