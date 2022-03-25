using Application.DTO;
using Application.MoviesService;
using Digiturk.Utils;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Digiturk.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class MoviesController : BaseController
	{

		private readonly IMovieService _moviesService;
		public MoviesController(IMovieService moviesService)
		{
			_moviesService = moviesService;
		}

		[HttpGet]
		[SwaggerResponse((int)HttpStatusCode.OK, typeof(ResponseModel<List<MovieDto>>))]
		[OpenApiOperation(summary: "Movies List", description: "Feches all movies.")]
		public async Task<ResponseModel<List<MovieDto>>> GetAll()
		{
			var result = await _moviesService.GetAllMovies();
			return base.Success<List<MovieDto>>(result);
		}

		[HttpGet("{id}")]
		[SwaggerResponse((int)HttpStatusCode.OK, typeof(ResponseModel<MovieDetailDto>))]
		[OpenApiOperation(summary: "Movie Detail", description: "- Shows movie detail for spesific Id.")]
		public async Task<ResponseModel<MovieDetailDto>> Get(int id)
		{
			var result = await _moviesService.GetMovie(id);
			return base.Success<MovieDetailDto>(result);
		}
	}
}
