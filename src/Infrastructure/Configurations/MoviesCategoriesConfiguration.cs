using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class MoviesCategoriesConfiguration : IEntityTypeConfiguration<MovieCategory>
	{
		public void Configure(EntityTypeBuilder<MovieCategory> builder)
		{
			builder.HasKey(sc => new { sc.MovieId, sc.CategoryId });

			builder.HasOne<Movie>(sc => sc.Movie)
					.WithMany(s => s.MoviesCategories)
					.HasForeignKey(sc => sc.MovieId);


			builder.HasOne<Category>(sc => sc.Category)
					.WithMany(s => s.MoviesCategories)
					.HasForeignKey(sc => sc.CategoryId);
		}
	}
}
