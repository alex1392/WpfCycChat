namespace CycChat.Core
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Spatial;

  public partial class ChatRoom
  {
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    public string Owner { get; set; }

    [Required]
    [StringLength(50)]
    public string FriendName { get; set; }

    [Required]
    public string LastChat { get; set; }

    public DateTime Time { get; set; }
  }
}
