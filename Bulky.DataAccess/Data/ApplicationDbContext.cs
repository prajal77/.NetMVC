﻿using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options ): base  (options) 
        {
            
        }
        //create table
        public DbSet<Category> Categories { get; set; }

        // Seed entity onto the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,Name= "Action",DisplayOrder=1
                },
                 new Category
                 {
                     Id = 2,
                     Name = "Sci-fi",
                     DisplayOrder = 2
                 },
                  new Category
                  {
                      Id = 3,
                      Name = "History",
                      DisplayOrder = 3
                  }
                );
        }
    }
}