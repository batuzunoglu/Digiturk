using Application.Interceptor.Selector;
using Application.Jwt;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Modules;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;

namespace Application
{
	public class ApplicationModule : BaseModule
	{
		public ApplicationModule(IConfiguration Configuration) : base(Configuration)
		{

		}
		public override void InitContainer(ContainerBuilder builder)
		{
			builder.RegisterType<JwtHandler>().As<IJwtHandler>();

			//Auto Inject
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
					 .Where(type => type.GetInterfaces().Contains(typeof(IBaseService)))
					 .AsImplementedInterfaces()
					 .EnableInterfaceInterceptors(new ProxyGenerationOptions()
					 {
						 Selector = new InterceptorSelector()
					 });
		}
	}
}
