using Application.Interfaces;
using Core.IoC;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Context
{
	public static class ApplicationDbContextSeed
	{
		public static void SeedDefaultUserAsync(ApplicationDbContext context)
		{
			var admin = new User { Name = "Batuhan", Surname = "Uzunoglu", UserName = "admin" };
			if (context.Users.FirstOrDefault(u => u.UserName == admin.UserName) == null)
			{
				admin.SetPassword("password123");
				admin.MakeAdmin();
				context.Users.Add(admin);
			}

			var user = new User { Name = "Ali", Surname = "Veli", UserName = "user" };
			if (context.Users.FirstOrDefault(u => u.UserName == user.UserName) == null)
			{
				user.SetPassword("password123");
				context.Users.Add(user);
			}

			context.SaveChanges();
		}

		public static void SeedSampleDataAsync(ApplicationDbContext context)
		{
			if (!context.Categories.Any())
			{
				context.Categories.AddRange(
						new Category()
						{
							Name = "Action"
						},
						new Category()
						{
							Name = "Adventure"
						},
						new Category()
						{
							Name = "Sci-Fi"
						},
						new Category()
						{
							Name = "Animation"
						},
						new Category()
						{
							Name = "Comedy"
						}
				);
				context.SaveChanges();
			}

			// Seed, if necessary
			if (!context.Movies.Any())
			{
				var action = context.Categories.First(x => x.Name == "Action");
				var comedy = context.Categories.First(x => x.Name == "Comedy");
				var sciFi = context.Categories.First(x => x.Name == "Sci-Fi");
				var animation = context.Categories.First(x => x.Name == "Animation");
				var adventure = context.Categories.First(x => x.Name == "Adventure");

				context.Movies.AddRange(
						new Movie()
						{
							Title = "The Batman (2022)",
							ImdbRating = 8.4,
							Poster = "https://m.media-amazon.com/images/M/MV5BZjlmYjY0MTEtYjNmOC00OWQ5LWEyMGEtYzYxYTc2MjI4MTVjXkEyXkFqcGdeQXVyMzMwOTU5MDk@._V1_FMjpg_UX729_.jpg",
							StoryLine = "When the Riddler, a sadistic serial killer, begins murdering key political figures in Gotham, Batman is forced to investigate the city's hidden corruption and question his family's involvement.",
							Actors = new[] { "Robert Pattinson", "Zoe Kravitz", "Jeffrey Wright" },
							ReleaseDate = Convert.ToDateTime("2022-02-14"),
							MoviesCategories = new List<MovieCategory>(){
														new MovieCategory()
														{
																CategoryId = action.Id
														},
														new MovieCategory()
														{
																CategoryId= adventure.Id
														},
														new MovieCategory()
														{
																CategoryId= sciFi.Id
														}
								},
							VideoUrl = "https://www.youtube.com/watch?v=mqqft2x_Aa4&ab_channel=WarnerBros.Pictures",
						},
						new Movie()
						{
							Title = "The Godfather",
							ImdbRating = 9.2,
							Poster = "https://m.media-amazon.com/images/M/MV5BNjFiZjQ4ZTktMDllYS00M2UxLWJkY2MtOTk3NGZjZTg2MTlhXkEyXkFqcGdeQXVyOTU2NDAzNDE@._V1_FMjpg_UX500_.jpg",
							StoryLine = "The aging patriarch of an organized crime dynasty in postwar New York City transfers control of his clandestine empire to his reluctant youngest son.",
							Actors = new[] { "Marlon Brando", "Al Pacino", "James Caan" },
							ReleaseDate = Convert.ToDateTime("1972-03-23"),
							MoviesCategories = new List<MovieCategory>(){
														new MovieCategory()
														{
																CategoryId = animation.Id
														},
														new MovieCategory()
														{
																CategoryId= adventure.Id
														},
														new MovieCategory()
														{
																CategoryId= comedy.Id
														}
								},
							VideoUrl = "https://www.youtube.com/watch?v=sY1S34973zA&ab_channel=Fan-MadeFilmTrailers",
						}
				);

				context.SaveChanges();
			}
		}
	}
}
