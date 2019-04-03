namespace CycChat
{
  using System;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;

  public partial class DataBaseModel : DbContext
  {
    public DataBaseModel()
        : base("name=DataBaseModel")
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
    }
  }
}
