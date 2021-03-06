﻿using Core.Entities;
using Core.EntitiesBuilder;
using Microsoft.EntityFrameworkCore;
using Repository.EventsPortalContext;

namespace Repository
{
    public class EventsPortalDbContext : DbContext
    {
        public DbSet<Visit> Visits { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<User> Users { get; set; }

        public EventsPortalDbContext(DbContextOptions<EventsPortalDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new TypeBuilder(modelBuilder.Entity<EventType>());
            new UserBuilder(modelBuilder.Entity<User>());
            new VisitBuilder(modelBuilder.Entity<Visit>());
            new EventBuilder(modelBuilder.Entity<Event>());

            ModelBuilderExtentions.Seed(modelBuilder);
        }
    }
}
