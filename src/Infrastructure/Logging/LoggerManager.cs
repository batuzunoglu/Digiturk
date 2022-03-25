using Application.Interfaces;
using System;

namespace Infrastructure.Logging
{
	public class LoggerManager : ILogService
	{
		public void LogWarning(string name, string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("-----------------------LOG----------------------");
			Console.WriteLine(name);
			Console.WriteLine(message);
			Console.WriteLine("-----------------------LOG----------------------");
		}

		public void LogInformation(string name, string message)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("-----------------------LOG----------------------");
			Console.WriteLine(name);
			Console.WriteLine(message);
			Console.WriteLine("-----------------------LOG----------------------");
		}
	}
}
