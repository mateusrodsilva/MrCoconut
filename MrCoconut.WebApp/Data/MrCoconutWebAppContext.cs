using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MrCoconut.WebApp.Models;

namespace MrCoconut.WebApp.Data
{
    public class MrCoconutWebAppContext : DbContext
    {
        public MrCoconutWebAppContext (DbContextOptions<MrCoconutWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<MrCoconut.WebApp.Models.User> User { get; set; } = default!;
    }
}
