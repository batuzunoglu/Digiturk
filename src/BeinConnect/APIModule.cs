using Autofac;
using Core.Modules;
using Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using System.Linq;
using System.Text;

namespace Digiturk
{
	public class APIModule : BaseModule
	{
		public APIModule(IConfiguration Configuration) : base(Configuration)
		{

		}

		public override void InitService(IServiceCollection services)
		{
			services.AddOpenApiDocument(configure =>
			{
				configure.Title = "Digiturk API";
				configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme()
				{
					Type = OpenApiSecuritySchemeType.ApiKey,
					Name = "Authorization",
					In = OpenApiSecurityApiKeyLocation.Header,
					Description = "Enter valid token in the text input below.\r\n\r\nExample: \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
				});
			});
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(cfg =>
			{
				var options = Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
				cfg.TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
					ValidIssuer = options.Issuer,
					ValidAudience = options.ValidAudience,
					ValidateAudience = options.ValidateAudience,
					ValidateLifetime = options.ValidateLifetime
				};
			});

			services.AddCors();
		}

		public override void InitContainer(ContainerBuilder builder)
		{
			builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
		}
	}
}
