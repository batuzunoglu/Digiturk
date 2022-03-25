using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Modules
{
	public class BaseModule : Module
	{
		protected IConfiguration Configuration;
		public BaseModule(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public virtual void InitService(IServiceCollection services) { }
		public virtual void InitContainer(ContainerBuilder builder) { }

		protected override void Load(ContainerBuilder builder)
		{
			IServiceCollection services = new ServiceCollection();
			InitService(services);
			builder.Populate(services);
			InitContainer(builder);
		}
	}
}
