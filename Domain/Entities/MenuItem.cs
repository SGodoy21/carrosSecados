using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MenuItem
{
    public int Id { get; set; }

    public string Label { get; set; }

    public string Icon { get; set; }

    public string RouterLink { get; set; }

    public int ParentId { get; set; }

    public int? Orden { get; set; }

    public bool? Activo { get; set; }
}
