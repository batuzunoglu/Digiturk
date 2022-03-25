namespace Digiturk.Utils
{


	public class ResponseModel<T>
			where T : class, new()
	{
		public T Data { get; set; }
		public bool IsSuccess { get; }
		public string Message { get; }
		public ResponseModel(bool isSuccess, T data = null, string message = "")
		{
			Data = data;
			IsSuccess = isSuccess;
			Message = message;
		}
	}
	public class ResponseModel
	{
		public bool IsSuccess { get; }
		public string Message { get; }
		public ResponseModel(bool isSuccess, string message = "")
		{
			IsSuccess = isSuccess;
			Message = message;
		}
	}
}
