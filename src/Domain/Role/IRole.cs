using System.Collections.Generic;

namespace Domain.Role
{
	public interface IRole
	{
		string GetRole();
		List<string> RolePermission();
	}
}
