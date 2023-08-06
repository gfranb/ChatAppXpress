using ChatAppXpress.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAppXpress.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(contacts => contacts.Contacts)
                .WithMany();

            modelBuilder.Entity<Message>()
                .HasOne(user => user.Sender)
                .WithMany(messages => messages.SentMessages)
                .HasForeignKey(user => user.SenderId);

            modelBuilder.Entity<Message>()
                .HasOne(user => user.Receiver)
                .WithMany(messages => messages.ReceiveMessages)
                .HasForeignKey(user => user.ReceiverId);
        }
    }
}
