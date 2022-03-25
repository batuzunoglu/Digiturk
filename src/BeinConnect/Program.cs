using Autofac.Extensions.DependencyInjection;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Digiturk
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			try
			{
				using (var scope = host.Services.CreateScope())
				{
					var services = scope.ServiceProvider;

					var context = services.GetRequiredService<ApplicationDbContext>();

					ApplicationDbContextSeed.SeedSampleDataAsync(context);
					ApplicationDbContextSeed.SeedDefaultUserAsync(context);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				host.Run();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
					.UseServiceProviderFactory(new AutofacServiceProviderFactory())
					.ConfigureWebHostDefaults(webBuilder =>
					{
						webBuilder.UseStartup<Startup>();
					});
	}
}
