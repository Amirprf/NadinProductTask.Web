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

		#region Identity

		public const string OkRegisteredUser = "ثبت نام کاربر با موفقیت انجام شد";
		public const string ExiststUser = "کاربری با این مشخصات در سامانه وجود دارد!";
		public const string RegisterUserSomethingWhentWrong = "عملیات ثبت کاربر با مشکل مواجه شد! لطفا اطلاعات کاربر را بررسی کرده و مجدد اقدام کنید.";




		public const string Success = "Success";
		public const string Error = "Error";

		#endregion

		public const string YouDontHaveAccess = "شما دسترسی های لازم برای انجام این کار را ندارید";
	}
}

