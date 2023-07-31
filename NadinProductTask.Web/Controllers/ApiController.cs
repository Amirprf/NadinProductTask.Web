using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace NadinProductTask.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ApiController : ControllerBase
	{

		[NonAction]
		public IActionResult OkResult(string message)
		{
			return Ok(new ResponseMessage(message, null));
		}

		[NonAction]
		public IActionResult OkResult(string message, object content)
		{
			return Ok(new ResponseMessage(message, content));
		}

		protected bool IsAllNullOrEmpty(object myObject)
		{
			foreach (PropertyInfo pi in myObject.GetType().GetProperties())
			{
				if (pi.PropertyType == typeof(string))
				{
					string value = (string)pi.GetValue(myObject);
					if (!string.IsNullOrEmpty(value))
					{
						return false;
					}
				}
			}
			return true;
		}

		private class ResponseMessage
		{
			public ResponseMessage(string message, object content)
			{
				Message = message;
				Content = content;
			}
			public string Message { get; set; }
			public object Content { get; set; }
		}
	}


	public static class ApiMessage
	{
		public const string IdIsNotValid = "شناسه کاربری وارد شده معتبر نمیباشد";


		#region Product

		public const string EmailOrProduceDateExists = "ایمیل یا تاریخ معرفی تکراری میباشند";
		public const string OKProductDeleted = "محصول با موفقیت حذف شد";
		public const string OkGetProduct = " مشخصات محصول ";

		#endregion


		public const string YouDontHaveAccess = "شما دسترسی های لازم برای انجام این کار را ندارید";
        public const string Success = "Success";
		public const string Error = "Error";
	}
}

