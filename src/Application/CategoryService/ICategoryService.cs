using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.CategoryService
{
	public interface ICategoryService : IBaseService
	{
		/// <summary>
		/// Method that returns all categories.
		/// </summary>
		/// <returns>Returns all categories as CategoryDto list.</returns>
		Task<List<CategoryDto>> GetCategories();

		/// <summary>
		/// Method that returns category with movies by category Id.
		/// </summary>
		/// <param name="id">Represents Category Id.</param>
		/// <returns>Returns category with movies by category Id.</returns>
		Task<CategoryDetailDto> GetCategory(int id);
	}

}
