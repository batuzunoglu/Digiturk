using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.MoviesService
{
	public interface IMovieService : IBaseService
	{

		/// <summary>
		/// Method that returns all Movies.
		/// </summary>
		/// <returns>Returns all Movies.</returns>
		Task<List<MovieDto>> GetAllMovies();

		/// <summary>
		/// Method that returns movie detail.
		/// </summary>
		/// <param name="id">Represents movie Id.</param>
		/// <returns>Returns movie detail.</returns>
		Task<MovieDetailDto> GetMovie(int id);
	}
}
