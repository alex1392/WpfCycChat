namespace CycChat.Core
{
  using System;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;

  public partial class CycChatDbContext : DbContext
  {
    public CycChatDbContext()
        : base("name=CycChatDbContext")
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Friend> Friends { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
    }
  }
}
