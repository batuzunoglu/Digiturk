using Autofac;
using Core.Modules;
using Domain.RoleService;
using Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
	public class DomainModule : BaseModule
	{
		public DomainModule(IConfiguration Configuration) : base(Configuration)
		{

		}

		public override void InitService(IServiceCollection services)
		{
			var jwtSettins = Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
			services.AddSingleton(x => jwtSettins);

			var redisSettings = Configuration.GetSection(nameof(RedisConfiguration)).Get<RedisConfiguration>();
			services.AddSingleton(y => redisSettings);
		}
		public override void InitContainer(ContainerBuilder builder)
		{
			builder.RegisterType<RoleManager>().As<IRoleService>();
		}
	}
}
