using Digiturk.Identity.Models;
using Domain.Role;
using Domain.RoleService;
using Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Jwt
{
	public class JwtHandler : IJwtHandler
	{
		private readonly JwtOptions _options;
		private readonly SymmetricSecurityKey _issuerSigningKey;
		private readonly IRoleService _roleService;
		public JwtHandler(
				JwtOptions options,
				IRoleService roleService
				)
		{
			_roleService = roleService;
			_options = options;
			_issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
		}

		public string GenerateToken(int userId, string userName, string role)
		{
			var now = DateTime.UtcNow;
			var jwtClaims = new List<Claim>
						{
								new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
								new Claim(JwtRegisteredClaimNames.UniqueName, userName),
								new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
								new Claim(JwtRegisteredClaimNames.Iat, now.ToString()),
						};
			if (!string.IsNullOrWhiteSpace(role))
			{
				jwtClaims.Add(new Claim(ClaimTypes.Role, role));
				IRole currentRole = _roleService.GetRole(role);
				foreach (var perm in currentRole.RolePermission())
				{
					jwtClaims.Add(new Claim("perm", perm));
				}
			}

			var expires = now.AddMinutes(_options.ExpiryMinutes);
			SigningCredentials signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);

			var jwt = new JwtSecurityToken(
					issuer: _options.Issuer,
					claims: jwtClaims,
					notBefore: now,
					expires: expires,
					audience: _options.ValidAudience,
					signingCredentials: signingCredentials
			);

			var token = new JwtSecurityTokenHandler().WriteToken(jwt);
			return token;
		}

		public TokenPayload TokenParser(string token)
		{
			TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
			{
				IssuerSigningKey = _issuerSigningKey,
				ValidIssuer = _options.Issuer,
				ValidAudience = _options.ValidAudience,
				ValidateAudience = _options.ValidateAudience,
				ValidateLifetime = _options.ValidateLifetime
			};
			JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			try
			{
				jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedSecurityToken);
				if (!(validatedSecurityToken is JwtSecurityToken jwt))
				{
					throw new UnauthorizedAccessException("Wrong token");
				}

				return new TokenPayload()
				{
					Role = jwt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
					UserId = Convert.ToInt32(jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value)
				};
			}
			catch
			{
				throw new UnauthorizedAccessException("Token is expired.");
			}
		}

	}
}
