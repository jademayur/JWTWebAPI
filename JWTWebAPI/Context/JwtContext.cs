﻿using JWTWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTWebAPI.Context
{
    public class JwtContext : DbContext
    {
        public JwtContext(DbContextOptions<JwtContext> options): base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        
    }
}
