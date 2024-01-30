using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HobbyCollection.Models;

namespace HobbyCollection.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext (DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        public DbSet<HobbyCollection.Models.Favorite> Favorite { get; set; } = default!;
    }
}
