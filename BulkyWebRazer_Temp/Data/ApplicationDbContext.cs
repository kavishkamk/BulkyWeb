﻿using BulkyWebRazer_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazer_Temp.Data
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
		
		}

		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
				new Category { CategoryId = 2, Name = "SkyFy", DisplayOrder = 2 },
				new Category { CategoryId = 3, Name = "History", DisplayOrder = 3}
			);

		}


	}
}
