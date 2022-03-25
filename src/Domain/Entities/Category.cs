using Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class Category : Entity<int>
	{
		[Required]
		public string Name { get; set; }
		public IList<MovieCategory> MoviesCategories { get; set; }
	}
}
