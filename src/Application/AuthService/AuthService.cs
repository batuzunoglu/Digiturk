using Application.DTO;
using Application.Exceptions;
using Application.Interceptors.Attributes;
using Application.Interfaces;
using Application.Jwt;
using Domain.Entities;
using Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Application.AuthService
{

	public class AuthService : IAuthService
	{
		private readonly IApplicationDbContext _context;
		private readonly IJwtHandler _jwtHandler;
		private readonly ICacheService _cacheService;
		private readonly JwtOptions _options;
		private readonly IHttpContextAccessor _httpContext;

		public AuthService(
						IApplicationDbContext context,
						IJwtHandler jwtHandler,
						ICacheService cacheService,
						IHttpContextAccessor httpContext,
						JwtOptions options
				)
		{
			_httpContext = httpContext;
			_context = context;
			_jwtHandler = jwtHandler;
			_cacheService = cacheService;
			_options = options;

		}

		/// <summary>
		/// Method that finds user from context, generate a token and sets to redis.
		/// </summary>
		/// <param name="model">Represents LoginDto</param>
		/// <returns>Returns token for user.</returns>
		public async Task<string> LoginAsync(LoginDto model)
		{
			User user = await _context.Users
				.Where(x => x.UserName == model.UserName)
				.FirstOrDefaultAsync();

			if (user == null)
			{
				throw new AuthenticationException($"Not found {nameof(model.UserName)}");
			}

			if (!user.ValidatePassword(model.Password))
			{
				throw new AuthenticationException("Wrong Password");
			}

			var jwt = _jwtHandler.GenerateToken(user.Id, user.UserName, user.Role);
			var key = $"User_{user.Id}";
			await _cacheService.SetAsync(key, jwt, TimeSpan.FromMinutes(_options.ExpiryMinutes));
			return jwt;
		}

		/// <summary>
		/// Method that removes redis token key for logged id user.
		/// </summary>
		[Auth]
		public async Task LogoutAsync()
		{
			var token = _httpContext.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
			var userId = _jwtHandler.TokenParser(token).UserId;

			var key = $"User_{userId}";
			var cacheToken = await _cacheService.GetAsync(key);
			if (cacheToken == token)
			{
				await _cacheService.RemoveAsync(key);
			}
		}

	}
}
