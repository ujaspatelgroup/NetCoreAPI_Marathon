using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarathonWebApiCore.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }
        public virtual DbSet<Events> Event { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
    }
}
