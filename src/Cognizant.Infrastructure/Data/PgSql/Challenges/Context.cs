using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cognizant.Infrastructure.Data.PgSql.Challenges
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
            // migration?
            // seed?
            // assure?
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// The challenges.
        /// </summary>
        public DbSet<Entities.Challenge> Challenges { get; set; }

        /// <summary>
        /// The users.
        /// </summary>
        public DbSet<Entities.User> Users { get; set; }
    }
}
