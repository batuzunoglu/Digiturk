namespace Digiturk.Utils
{
	public class ResponseErrorModel : ResponseModel
	{
		public string Source { get; set; }
		public ResponseErrorModel(string source, string message) : base(false, message: message)
		{
			Source = source;
		}
	}
}
