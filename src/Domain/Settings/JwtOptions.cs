namespace Domain.Settings
{
	public class JwtOptions
	{
		public int ExpiryMinutes { get; set; }
		public string SecretKey { get; set; }
		public string Issuer { get; set; }
		public bool ValidateLifetime { get; set; }
		public bool ValidateAudience { get; set; }
		public string ValidAudience { get; set; }
	}
}
