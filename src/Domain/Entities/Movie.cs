using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
	public class Movie : Entity<int>
	{
		public string Title { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string Poster { get; set; }
		public double ImdbRating { get; set; }
		public string StoryLine { get; set; }
		public ICollection<string> Actors { get; set; }
		public IList<MovieCategory> MoviesCategories { get; set; }
		public string VideoUrl { get; set; }

		public Movie()
		{
			Actors = new HashSet<string>();
		}
	}
}
