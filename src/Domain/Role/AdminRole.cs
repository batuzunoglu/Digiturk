using Domain.Consts;
using System.Collections.Generic;

namespace Domain.Role
{
	public class AdminRole : IRole
	{
		public string GetRole()
		{
			return nameof(AdminRole);
		}

		public List<string> RolePermission() => new List<string>(){
								Permission.Get_All_Users,
								Permission.Get_User,
								Permission.Make_Admin,
								Permission.Create_Category,
								Permission.Delete_Category,
								Permission.Movie_Detail
						};
	}
}
