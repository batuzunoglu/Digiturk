using Application.AuthService;
using Application.DTO;
using Digiturk.Utils;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace Digiturk.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthController : BaseController
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("CreateToken")]
		[SwaggerResponse((int)HttpStatusCode.OK, typeof(ResponseModel<LoginResponse>))]
		[OpenApiOperation(summary: "User Login", description: "Redis keeps the logined users tokens.\n\nDefault Users:\n\n\tUsername: admin\n\tPassword: password123\n\tDefault Role: Admin\n------\n\tUsername: user\n\tPassword: password123\n\tDefault Role: User")]
		public async Task<ResponseModel<LoginResponse>> Login([FromBody] LoginDto input)
		{
			var token = await _authService.LoginAsync(input);
			return base.Success<LoginResponse>(new LoginResponse { Token = token });
		}

		[HttpGet("Logout")]
		[OpenApiOperation(summary: "User Logout", description: "Remove token from redis and logout for user.")]
		[SwaggerResponse((int)HttpStatusCode.OK, typeof(ResponseModel))]
		public async Task<ResponseModel> Logout()
		{
			await _authService.LogoutAsync();
			return base.Success();
		}
	}
}
