using AutoMapper;
using NadinProductTask.Application.Commands.ProductCommands;
using NadinProductTask.Application.Dtos;
using NadinProductTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Helpers
{
	public class MappingProfiles: Profile
	{
		public MappingProfiles()
		{
			CreateMap<AddProductCommand, Product>()
				.ForMember
				(dest=>
					dest.ManufactureEmail, opt => 
					opt.MapFrom(src=>
					src.ManufactureEmail.ToLower()
				));
			
			CreateMap<Product,ProductDto>();
		}
	}
}
