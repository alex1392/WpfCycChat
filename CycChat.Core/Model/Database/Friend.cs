﻿namespace CycChat.Core
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Spatial;

  public partial class Friend
  {
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    public string A { get; set; }

    [Required]
    [StringLength(50)]
    public string B { get; set; }
  }
}
