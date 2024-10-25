﻿using FinanceWebSite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceWebSite.API.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
           
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
