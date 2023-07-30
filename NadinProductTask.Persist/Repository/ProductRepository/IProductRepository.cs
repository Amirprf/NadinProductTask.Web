using NadinProductTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Persist.Repository.ProductRepository
{
	public interface IProductRepository
	{
		Task AddAsync(Product product);
	}
}
