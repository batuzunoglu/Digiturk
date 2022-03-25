using Application.Interfaces;
using Autofac;
using Core.Modules;
using Infrastructure.Context;
using Infrastructure.Logging;
using Infrastructure.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
	public class InfrastructureModule : BaseModule
	{
		public InfrastructureModule(IConfiguration Configuration) : base(Configuration)
		{

		}
		public override void InitService(IServiceCollection services)
		{

			services.AddDbContext<ApplicationDbContext>(options =>
					options.UseInMemoryDatabase("Digiturk"));
		}
		public override void InitContainer(ContainerBuilder builder)
		{
			builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>().InstancePerLifetimeScope();
			builder.RegisterType<LoggerManager>().As<ILogService>().InstancePerLifetimeScope();
			builder.RegisterType<RedisCacheManager>().As<ICacheService>().SingleInstance();
		}
	}
}
