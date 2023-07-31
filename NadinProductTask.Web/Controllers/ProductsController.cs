using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NadinProductTask.Application.Commands.ProductCommands;
using NadinProductTask.Application.Services.ProductServices;
using NadinProductTask.Application.Validators.Product;
using System.Security.Claims;
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

		/// <summary>
		/// افزودن محصول
		/// </summary>
		[HttpPost, Authorize]
		public async Task<IActionResult> AddProductAsync([FromBody] AddProductCommand command)
		{
			command.AthorUserName = User.Identity.Name;

			var error = command.ExecuteError();

			if (!command.Validate())
			{
				return BadRequest(error);
			}
			try
			{
				await _productService.AddProductAsync(command);
			}
			catch (InvalidOperationException)
			{
				return Conflict(ApiMessage.EmailOrProduceDateExists);
			}
			

			return OkResult(ApiMessage.Success);
		}

		/// <summary>
		/// ویرایش محصول
		/// </summary>
		[HttpPut]
		public async Task<IActionResult> EditProductAsync([FromBody] EditProductCommand command)
		{
			var error = command.ExecuteError();

			// TODO: Getting Users userName From Claims and fill our command
		

			if (!command.Validate())
			{
				return BadRequest(error);
			}

			try
			{
				await _productService.UpdateProductById(command);
			}
			catch (AccessViolationException)
			{
				return Forbid(ApiMessage.YouDontHaveAccess);
			}
			catch(InvalidOperationException)
			{
				return Conflict(ApiMessage.EmailOrProduceDateExists);
			}
			

			return OkResult(ApiMessage.Success);
		}

		/// <summary>
		/// حذف محصول
		/// </summary>
		[HttpDelete]
		public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductCommand command)
		{
			var error = command.ExecuteError();

			//TODO : authorUserName => command
			

			if (!command.Validate())
			{
				return BadRequest(error);
			}
			var res = await _productService.DeleteProduct(command);

			if(res == 2)
			{
				return Forbid(ApiMessage.YouDontHaveAccess);
			}
			if (res == 0)
			{
				return NotFound();
			}
			return OkResult(ApiMessage.OKProductDeleted);
		}

		/// <summary>
		/// نمایش محصول به وسیله آیدی
		/// </summary>
		/// /// <param name="id">آیدی محصول</param>
		/// <returns>محصول</returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductForEdit(Guid id)
		{

			var product = await _productService.GetProductById(id);

			return OkResult(ApiMessage.OkGetProduct, product);
		}
	}
}
