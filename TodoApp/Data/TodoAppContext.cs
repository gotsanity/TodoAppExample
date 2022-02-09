#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodoAppContext : IdentityDbContext<AppUser>
    {
        public TodoAppContext (DbContextOptions<TodoAppContext> options)
            : base(options)
        {
        }

        public DbSet<TodoApp.Models.Todo> Todo { get; set; }

        public DbSet<TodoApp.Models.Location> Location { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
