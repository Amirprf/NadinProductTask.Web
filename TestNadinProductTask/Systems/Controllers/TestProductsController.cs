using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NadinProductTask.Application.Commands.ProductCommands;
using NadinProductTask.Application.Dtos;
using NadinProductTask.Application.Services.ProductServices;
using NadinProductTask.Domain.Entities;
using NadinProductTask.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNadinProductTask.Fixtures;

namespace TestNadinProductTask.Systems.Controllers
{
	public class TestProductsController
	{
		[Fact]
		public async Task GetAll_OnSuccess_ReturnStatusCode200()
		{
			//Arrange
			var mockProductsService = new Mock<IProductService>();

			mockProductsService
				.Setup(service => service.GetAllProducts())
				.ReturnsAsync(ProductsFixture.GetTestProducts());

			var sut = new ProductsController(mockProductsService.Object);
			//Act
			var result = (OkObjectResult)await sut.GetAll();

			//Assert
			result.StatusCode.Should().Be(200);
		}

		[Fact]
		public async Task GetAll_OnSuccess_InvokesUserServiceExactlyOnce()
		{
			//Arrange
			var mockProductsService = new Mock<IProductService>();

			mockProductsService
				.Setup(service => service.GetAllProducts())
				.ReturnsAsync(new List<ProductDto>());

			var sut = new ProductsController(mockProductsService.Object);

			//Act
			var result = await sut.GetAll();

			//Assert
			mockProductsService.Verify(service => service.GetAllProducts(), Times.Once());
		}

		[Fact]
		public async Task GetAll_OnSuccess_ReturnsListOfProducts()
		{
			//Arrange
			var mockProductsService = new Mock<IProductService>();

			mockProductsService
				.Setup(service => service.GetAllProducts())
				.ReturnsAsync(ProductsFixture.GetTestProducts());

			var sut = new ProductsController(mockProductsService.Object);

			//Act
			var result = await sut.GetAll();

			//Assert
			result.Should().BeOfType<OkObjectResult>();
			var ObjectResult = (OkObjectResult)result;
			ObjectResult.Value.Should().BeOfType<List<ProductDto>>();
		}

		[Fact]
		public async Task GetAll_OnNotProductsFound_Returns404()
		{
			//Arrange
			var mockProductsService = new Mock<IProductService>();

			mockProductsService
				.Setup(service => service.GetAllProducts())
				.ReturnsAsync(new List<ProductDto>());

			var sut = new ProductsController(mockProductsService.Object);

			//Act
			var result = await sut.GetAll();

			//Assert
			result.Should().BeOfType<NotFoundResult>();
			var ObjectResult = (NotFoundResult)result;
			ObjectResult.StatusCode.Should().Be(404);
		}

		[Fact]
		public async Task AddProduct_OnSuccess_ReturnStatusCode200()
		{
			var productCommand = new AddProductCommand() 
			{
				AthorUserName="User1",
				IsAvailable=true,
				ManufactureEmail="mani@mail.com",
				ManufacturePhone="123456789",
				Name="Product1",
				ProduceDate=DateTime.Now,
			};

			var mockProductsService = new Mock<IProductService>();

			mockProductsService
			.Setup(service => service.AddProductAsync(productCommand));

			var sut = new ProductsController(mockProductsService.Object);
			 
			var result = (OkObjectResult)await sut.AddProduct(productCommand);
			//Assert
			result.StatusCode.Should().Be(200);
		}

	}
}
