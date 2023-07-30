using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NadinProductTask.Application.Services.ProductServices;
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
	}
}
