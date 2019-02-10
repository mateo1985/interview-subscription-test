using MailSubscriptionsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MailSubscriptionsApi.Data
{
    public class SubscriptionsContext: DbContext
    {
        public SubscriptionsContext(DbContextOptions<SubscriptionsContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.Subscriptions).WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Topic>().Property(x => x.TopicId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Topic>().HasData(new Topic[]
            {
                new Topic {DisplayName = "ASP.NET Core", TopicId = 1},
                new Topic {DisplayName = "Docker", TopicId = 2},
                new Topic {DisplayName = "TypeScript", TopicId = 3},
                new Topic {DisplayName = "Vanilla JS", TopicId = 4},
                new Topic {DisplayName = "Adobe Experience Manager", TopicId = 5},
                new Topic {DisplayName = "Node.js", TopicId = 6}
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }
    }
}
