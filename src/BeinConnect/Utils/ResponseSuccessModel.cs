namespace Digiturk.Utils
{
	public class ResponseSuccessModel : ResponseModel
	{
		public ResponseSuccessModel() : base(true)
		{

		}
	}
	public class ResponseSuccessModel<T> : ResponseModel<T>
		where T : class, new()
	{
		public ResponseSuccessModel(T data) : base(true, data)
		{

		}
	}

}
