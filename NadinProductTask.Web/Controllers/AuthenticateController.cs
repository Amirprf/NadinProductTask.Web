using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NadinProductTask.Application.Commands.IdentityCommand;
using NadinProductTask.Domain.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NadinProductTask.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticateController : ApiController
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _configuration;

		public AuthenticateController(
		   UserManager<IdentityUser> userManager,
		   RoleManager<IdentityRole> roleManager,
		   IConfiguration configuration)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginCommand command)
		{

			var error = command.ExecuteError();
			if (!command.Validate())
			{
				return BadRequest(error);
			}

			var user = await _userManager.FindByNameAsync(command.Username);
			if (user != null && await _userManager.CheckPasswordAsync(user, command.Password))
			{
				var userRoles = await _userManager.GetRolesAsync(user);

				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(ClaimTypes.NameIdentifier,user.Id),
				};

				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}

				var token = GetToken(authClaims);

				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
					expiration = token.ValidTo
				});
			}
			return Unauthorized();
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterCommand command)
		{
			var error = command.ExecuteError();
			if (!command.Validate())
			{
				return BadRequest(error);
			}

			var userExists = await _userManager.FindByNameAsync(command.Username);
			if (userExists != null)
				return StatusCode(StatusCodes.Status500InternalServerError, ApiMessage.ExiststUser);

			IdentityUser user = new()
			{
				Email = command.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = command.Username
			};
			var result = await _userManager.CreateAsync(user, command.Password);
			if (!result.Succeeded)
				return StatusCode(StatusCodes.Status500InternalServerError, ApiMessage.RegisterUserSomethingWhentWrong);

			return OkResult(ApiMessage.OkRegisteredUser);
		}

		[HttpPost]
		[Route("register-admin")]
		public async Task<IActionResult> RegisterAdmin([FromBody] RegisterCommand command)
		{
			var error = command.ExecuteError();
			if (!command.Validate())
			{
				return BadRequest(error);
			}

			var userExists = await _userManager.FindByNameAsync(command.Username);
			if (userExists != null)
				return StatusCode(StatusCodes.Status500InternalServerError, ApiMessage.ExiststUser);

			IdentityUser user = new()
			{
				Email = command.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = command.Username
			};
			var result = await _userManager.CreateAsync(user, command.Password);
			if (!result.Succeeded)

				return StatusCode(StatusCodes.Status500InternalServerError, result.Errors.First()/*ApiMessage.RegisterUserSomethingWhentWrong*/);

			if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
				await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
			if (!await _roleManager.RoleExistsAsync(UserRoles.User))
				await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

			if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await _userManager.AddToRoleAsync(user, UserRoles.Admin);
			}
			if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await _userManager.AddToRoleAsync(user, UserRoles.User);
			}
			return OkResult(ApiMessage.OkRegisteredUser);
		}

		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddHours(1),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

			return token;
		}
	}
}