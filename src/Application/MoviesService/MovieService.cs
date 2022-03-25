using Application.DTO;
using Application.Exceptions;
using Application.Interceptors.Attributes;
using Application.Interfaces;
using Domain.Consts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.MoviesService
{
	public class MovieService : IMovieService
	{
		private readonly IApplicationDbContext _context;
		public MovieService(IApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Method that returns all Movies.
		/// </summary>
		/// <returns>Returns all Movies.</returns>
		[Logging]
		[Cache]
		public async Task<List<MovieDto>> GetAllMovies()
		{
			var result = await _context.Movies
				.Select(x => new MovieDto()
			{
				Id = x.Id,
				ImdbRating = x.ImdbRating,
				Poster = x.Poster,
				ReleaseDate = x.ReleaseDate,
				StoryLine = x.StoryLine,
				Title = x.Title,
				Actors = x.Actors.ToList()
			}).ToListAsync();
			return result;
		}

		/// <summary>
		/// Method that returns movie detail.
		/// </summary>
		/// <param name="id">Represents movie Id.</param>
		/// <returns>Returns movie detail.</returns>
		[Logging]
		[Auth(Permission.Movie_Detail)]
		public async Task<MovieDetailDto> GetMovie(int id)
		{
			var result = await _context.Movies
					.Include(x => x.MoviesCategories)
					.Select(x => new MovieDetailDto()
					{
						Id = x.Id,
						ImdbRating = x.ImdbRating,
						Poster = x.Poster,
						ReleaseDate = x.ReleaseDate,
						StoryLine = x.StoryLine,
						Title = x.Title,
						Actors = x.Actors.ToList(),
						Categories = x.MoviesCategories.Select(y => new CategoryDto()
						{
							Id = y.CategoryId,
							Name = y.Category.Name
						}).ToList(),
						VideoUrl = x.VideoUrl
					}).FirstOrDefaultAsync(y => y.Id == id);
			if (result == null)
			{
				throw new FoundException("Movie Not Fount");
			}
			return result;
		}
	}
}
