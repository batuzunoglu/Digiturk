using System;
using Domain.Role;
using System.Collections.Generic;
using System.Linq;

namespace Domain.RoleService
{
	public class RoleManager : IRoleService
	{
		public List<IRole> GetRoles() => new List<IRole>
				{
						new AdminRole(),
						new UserRole()
				};

		public IRole GetRole(string roleName)
		{
			var role = GetRoles().FirstOrDefault(x => x.GetRole() == roleName);
			if (role == null)
			{
				throw new ArgumentNullException(roleName);
			}
			return role;
		}
	}

}
