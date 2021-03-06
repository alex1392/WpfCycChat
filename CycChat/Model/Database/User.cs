namespace CycChat
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Spatial;

  public partial class User
  {
    public User()
    {

    }

    public User(string username, string password)
    {
      UserName = username;
      Password = password;
    }

    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }

  }
}
