using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NadinProductTask.Application.Helpers;
using NadinProductTask.Application.Services.ProductServices;
using NadinProductTask.Persist.Persist;
using NadinProductTask.Persist.Repository.ProductRepository;
using System.Text;

namespace NadinProductTask.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

			//Identity
			builder.Services.AddIdentity<IdentityUser, IdentityRole>()
			.AddEntityFrameworkStores<DatabaseContext>()
			.AddDefaultTokenProviders();

			//Adding Authentication
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			// Adding Jwt Bearer
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = builder.Configuration["JWT:ValidAudience"],
					ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))

					/*SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))*/
				};
			});


			builder.Services.AddTransient<IProductService, ProductService>();
			builder.Services.AddTransient<IProductRepository, ProductRepository>();

			//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			//	.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
			
			//Auto Mapper
			builder.Services.AddAutoMapper(typeof(MappingProfiles));

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();

			//Swagger
			builder.Services.AddSwaggerGen(opt =>
			{
				//Enabling Token header input in swagger
				opt.SwaggerDoc("v1", new OpenApiInfo { Title = "NadinApi", Version = "v1" });

				opt.SchemaFilter<NadinSwaggerSchemaFilter>();

				opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "bearer"
				});
				opt.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},new string[]{}
					}
				});
			});;


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}