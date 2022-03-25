using System;

namespace Application.DTO
{
	public class MovieLightDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string Poster { get; set; }
		public double ImdbRating { get; set; }
	}
}
