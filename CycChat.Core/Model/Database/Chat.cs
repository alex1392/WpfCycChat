namespace CycChat.Core
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Spatial;

  public partial class Chat
  {
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    public string Sender { get; set; }

    [Required]
    [StringLength(50)]
    public string Receiver { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime Time { get; set; }
  }
}
