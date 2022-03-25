using Domain.Consts;
using System.Collections.Generic;

namespace Domain.Role
{
	public class UserRole : IRole
	{
		public string GetRole()
		{
			return nameof(UserRole);
		}

		public List<string> RolePermission() => new List<string>(){
								Permission.Create_Category,
								Permission.Movie_Detail
		};
	}
}
