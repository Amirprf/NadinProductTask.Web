﻿using Microsoft.AspNetCore.Http;
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
}
