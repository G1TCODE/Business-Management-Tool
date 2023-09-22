using BMT.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher _thisPublisher;

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher) 
            : base(options)
        {
            _thisPublisher = publisher;
        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(thisDomain => thisDomain.Entity)
                .SelectMany(entityEvents =>
                {
                    var domainEvents = entityEvents.GetDomainEvents();

                    entityEvents.ClearDomainEvents();

                    return domainEvents;
                })
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _thisPublisher.Publish(domainEvent);
            }
        }
    }
}
