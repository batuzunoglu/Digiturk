using Domain.Role;
using System.Collections.Generic;

namespace Domain.RoleService
{
	public interface IRoleService
	{
		List<IRole> GetRoles();
		IRole GetRole(string roleName);
	}
}
