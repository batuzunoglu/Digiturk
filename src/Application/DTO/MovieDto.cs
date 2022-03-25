using System;
using System.Collections.Generic;

namespace Application.DTO
{
	public class MovieDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string Poster { get; set; }
		public double ImdbRating { get; set; }
		public string StoryLine { get; set; }
		public List<string> Actors { get; set; }
	}
}
