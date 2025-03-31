﻿using LoginRegister.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginRegister.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
    }
    
}
