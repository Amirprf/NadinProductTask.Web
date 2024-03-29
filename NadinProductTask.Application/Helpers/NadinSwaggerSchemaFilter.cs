﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NadinProductTask.Application.Helpers
{
	public class NadinSwaggerSchemaFilter : ISchemaFilter
	{
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			if (schema?.Properties == null)
			{
				return;
			}

			var ignoreDataMemberProperties = context.Type.GetProperties()
				.Where(t => t.GetCustomAttribute<IgnoreDataMemberAttribute>() != null);

			foreach (var ignoreDataMemberProperty in ignoreDataMemberProperties)
			{
				var propertyToHide = schema.Properties.Keys
					.SingleOrDefault(x => x.ToLower() == ignoreDataMemberProperty.Name.ToLower());

				if (propertyToHide != null)
				{
					schema.Properties.Remove(propertyToHide);
				}
			}
		}
	}
}