using Application.DTO;
using Application.Interceptors.Attributes;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;

namespace Application.CategoryService
{
	public class CategoryService : ICategoryService
	{
		private readonly IApplicationDbContext _context;
		public CategoryService(IApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Method that returns all categories.
		/// </summary>
		/// <returns>Returns all categories as CategoryDto list.</returns>
		[Logging]
		[Auth]
		[Cache]
		public async Task<List<CategoryDto>> GetCategories()
		{
			var result = await _context.Categories
				.Select(x => new CategoryDto()
				{
				Id = x.Id,
				Name = x.Name
				})
				.ToListAsync();

			return result;
		}

		/// <summary>
		/// Method that returns category with movies by category Id.
		/// </summary>
		/// <param name="id">Represents Category Id.</param>
		/// <returns>Returns category with movies by category Id.</returns>
		[Auth]
		[Cache]
		public async Task<CategoryDetailDto> GetCategory(int id)
		{
			var result = await _context.Categories
				 .Include(x => x.MoviesCategories)
				 .Select(y => new CategoryDetailDto()
				 {
					 Id = y.Id,
					 Name = y.Name,
					 Movies = y.MoviesCategories.Select(y => new MovieLightDto()
					 {
						 Id = y.MovieId,
						 ImdbRating = y.Movie.ImdbRating,
						 Poster = y.Movie.Poster,
						 ReleaseDate = y.Movie.ReleaseDate,
						 Title = y.Movie.Title
					 }).ToList()
				 })
				 .Where(y => y.Id == id)
				 .FirstOrDefaultAsync();

			if (result == null)
			{
				throw new FoundException("Not Found Category");
			}
			return result;
		}
	}
}
