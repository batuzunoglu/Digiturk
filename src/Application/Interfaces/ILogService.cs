namespace Application.Interfaces
{
	public interface ILogService
	{
		void LogWarning(string name, string message);
		void LogInformation(string name, string message);
	}
}
