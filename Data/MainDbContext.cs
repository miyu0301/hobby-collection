using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HobbyCollection.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HobbyCollection.Data
{
    public class MainDbContext : IdentityDbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        public DbSet<HobbyCollection.Models.Favorite> Favorite { get; set; } = default!;
        public DbSet<HobbyCollection.Models.User> User { get; set; } = default!;
    }
}
