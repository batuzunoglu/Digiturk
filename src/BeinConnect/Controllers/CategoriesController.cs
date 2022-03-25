using System;
using Application.CategoryService;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Digiturk.Utils;
using Microsoft.AspNetCore.Authorization;
using NSwag.Annotations;
using System.Net;

namespace Digiturk.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoriesController : BaseController
	{
		private readonly ICategoryService _categoryService;
		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		[SwaggerResponse((int)HttpStatusCode.OK, typeof(ResponseModel<List<CategoryDto>>))]
		[OpenApiOperation(summary: "Category List", description: "- Returns all categories.\n\n  - If no data exists in Redis fetches datas from db write to datas Redis and return.")]
		public async Task<ResponseModel<List<CategoryDto>>> GetAll()
		{
			var result = await _categoryService.GetCategories();
			return base.Success<List<CategoryDto>>(result);
		}

		[HttpGet("{id}")]
		[SwaggerResponse((int)HttpStatusCode.OK, typeof(ResponseModel<CategoryDetailDto>))]
		[OpenApiOperation(summary: "Categories and related movies", description: "Lists category and related movies with spesific Id.")]
		public async Task<ResponseModel<CategoryDetailDto>> Get(int id)
		{
			var result = await _categoryService.GetCategory(id);
			return base.Success<CategoryDetailDto>(result);
		}
	}
}
