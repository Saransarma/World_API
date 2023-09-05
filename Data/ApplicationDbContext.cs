﻿using Microsoft.EntityFrameworkCore;
using World_Api.Models;

namespace World_Api.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Country> countries { get; set; }
    }
}
