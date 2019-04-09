namespace CycChat.Core
{
  using CycWpfLibrary;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Spatial;

  public partial class ChatRoom : IData
  {
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    public string Owner { get; set; }

    [Required]
    [StringLength(50)]
    public string FriendName { get; set; }

    public string LastChat { get; set; }

    public DateTime? Time { get; set; }
  }
}
