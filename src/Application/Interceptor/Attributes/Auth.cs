using System;
using Application.Exceptions;
using Application.Interceptor.Base;
using Application.Interfaces;
using Application.Jwt;
using Castle.DynamicProxy;
using Core.IoC;
using Domain.Role;
using Domain.RoleService;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Application.Interceptors.Attributes
{
	public class Auth : InterceptorBaseAttribute
	{
		private readonly string _requiredPermission;
		private readonly IHttpContextAccessor _httpContext;
		private readonly IRoleService _roleService;
		private readonly ICacheService _cacheService;
		private readonly IJwtHandler _jwtHandler;
		public Auth(string permission = null)
		{
			_httpContext = IoCContainer.Resolve<IHttpContextAccessor>();
			_roleService = IoCContainer.Resolve<IRoleService>();
			_cacheService = IoCContainer.Resolve<ICacheService>();
			_jwtHandler = IoCContainer.Resolve<IJwtHandler>();
			_requiredPermission = permission;
		}

		public override void OnProceed(IInvocation invocation)
		{
			var bearer = _httpContext.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
			if (string.IsNullOrEmpty(bearer))
			{
				throw new UnauthorizedAccessException("Access Token needed.");
			}

			var tokenPayload = _jwtHandler.TokenParser(bearer);

			var key = $"User_{tokenPayload.UserId}";
			if (!_cacheService.Exits(key))
			{
				throw new UnauthorizedAccessException("Your access token expired.");
			}

			var cacheToken = _cacheService.Get<string>(key);
			if (bearer != cacheToken)
			{
				throw new UnauthorizedAccessException("Wrong token");

			}

			IRole currentRole = _roleService.GetRole(tokenPayload.Role);
			var permissions = currentRole.RolePermission();
			bool isCheckPermission = false;
			if (!string.IsNullOrEmpty(_requiredPermission))
			{
				foreach (var item in permissions)
				{
					if (_requiredPermission == item)
					{
						isCheckPermission = true;
						break;
					}
				}
				if (!isCheckPermission)
				{
					throw new AuthenticationException("You don't have permission", HttpStatusCode.Forbidden);
				}
			}
			invocation.Proceed();
		}

	}
}
