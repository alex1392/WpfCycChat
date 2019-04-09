namespace CycChat.Core
{
  using CycWpfLibrary;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Spatial;

  public partial class Chat : IData
  {
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    public string Sender { get; set; }

    [Required]
    [StringLength(50)]
    public string Receiver { get; set; }

    public string Content { get; set; }

    public DateTime? Time { get; set; }
  }
}
