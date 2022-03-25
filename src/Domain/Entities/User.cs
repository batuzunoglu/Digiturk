using System;
using System.ComponentModel.DataAnnotations;
using Core.IoC;
using Domain.Entities.Base;
using Domain.Role;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
	public class User : Entity<int>
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Role { get; private set; } = nameof(UserRole);
		[Required]
		public string UserName { get; set; }
		public string Password { get; private set; }


		public void SetPassword(string password)
		{
			CheckPassword(password);
			IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
			Password = passwordHasher.HashPassword(this, password);
		}

		public bool ValidatePassword(string password)
		{
			CheckPassword(password);
			IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
			var verify = passwordHasher.VerifyHashedPassword(this, this.Password, password);
			return verify != PasswordVerificationResult.Failed;
		}

		public void MakeAdmin()
		{
			this.Role = nameof(AdminRole);
		}

		private void CheckPassword(string password)
		{
			if (string.IsNullOrEmpty(password))
				throw new ArgumentNullException(nameof(password));
		}
	}
}