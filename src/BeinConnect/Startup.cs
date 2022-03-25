using Application;
using Autofac;
using Digiturk.Middlewares;
using Core.IoC;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Digiturk
{
	public class Startup
	{
		public IConfigurationRoot Configuration { get; private set; }

		public Startup(IWebHostEnvironment env)
		{
			var configBuilder = new ConfigurationBuilder()
					.SetBasePath(env.ContentRootPath)
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddEnvironmentVariables();
			this.Configuration = configBuilder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterModule(new APIModule(Configuration));
			builder.RegisterModule(new ApplicationModule(Configuration));
			builder.RegisterModule(new InfrastructureModule(Configuration));
			builder.RegisterModule(new DomainModule(Configuration));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			IoCContainer.Init(app.ApplicationServices);

			if (env.IsDevelopment())
			{
				app.UseOpenApi();
				app.UseSwaggerUi3();
			}

			app.UseCors(opt => opt
							.AllowAnyOrigin()
							.AllowAnyHeader()
							.AllowAnyMethod());

			app.UseHttpsRedirection();

			app.UseRouting();

			app.ConfigureExceptionHandler();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
