using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NadinProductTask.Application.Services.ProductServices;

namespace NadinProductTask.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ApiController
	{

		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		/// <summary>
		/// لیست محصولات
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var products = await _productService.GetAllProducts();

			if (!products.Any())
				return NotFound();

			return Ok(products);
		}
	}
}
