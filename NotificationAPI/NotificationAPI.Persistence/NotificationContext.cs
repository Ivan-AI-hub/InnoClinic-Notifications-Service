﻿using Microsoft.EntityFrameworkCore;
using NotificationAPI.Domain;
using NotificationAPI.Persistence.EntityConfigurations;

namespace NotificationAPI.Persistence
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfigurator());
        }
    }
}
