using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NadinProductTask.Application.Commands.ProductCommands;
using NadinProductTask.Application.Services.ProductServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		public async Task<IActionResult> GetAllProducts()
		{
			var products = await _productService.GetAllProducts();

			if (!products.Any())
				return NotFound();

			return OkResult(ApiMessage.Success,products);
		}

		[HttpPost]
		public async Task<IActionResult> AddProductAsync(AddProductCommand command)
		{
			// TODO: Getting Users userName From Claims and fill our command

			var error = command.ExecuteError();

			if (!command.Validate())
			{
				return BadRequest(error);
			}
			await _productService.AddProductAsync(command);

			return OkResult(ApiMessage.Success);
		}
	}
}
