namespace CycChat.Core
{
  using System;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;
  using CycWpfLibrary;
  using System.Data.Entity.Validation;

  public partial class AppDbContext : DbContextBase
  {
    public AppDbContext()
        : base("name=CycChatDbContext")
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Friend> Friends { get; set; }
    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Fluent API
      //modelBuilder.Entity<User>().HasKey(u => u.ID); // set key property for entity

    }
  }
}
