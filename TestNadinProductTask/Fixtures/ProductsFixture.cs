﻿using NadinProductTask.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNadinProductTask.Fixtures
{
	public static class ProductsFixture
	{
		public static List<ProductDto> GetTestProducts() => new() {
				new ProductDto
				{
					AthorUserName = "User1",
					Id = 1,
					IsAvailable = true,
					IsDelete = false,
					ManufactureEmail="Users1@Mail.com",
					ManufacturePhone="123456789",
					Name="Product1",
					ProduceDate=DateTime.Now
				},
				new ProductDto
				{
					AthorUserName = "User2",
					Id = 2,
					IsAvailable = true,
					IsDelete = false,
					ManufactureEmail="Users2@Mail.com",
					ManufacturePhone="123456789",
					Name="Product2",
					ProduceDate=DateTime.Today
				}
		};
	}
}
