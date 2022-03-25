using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IApplicationDbContext
	{
		DbSet<User> Users { get; set; }
		DbSet<Category> Categories { get; set; }
		DbSet<Movie> Movies { get; set; }
		DbSet<MovieCategory> MoviesCategories { get; set; }

		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
