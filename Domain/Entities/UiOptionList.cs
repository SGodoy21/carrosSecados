using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UiOptionList
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<UiOptionItem> UisOptionsItems { get; set; } = new List<UiOptionItem>();
}
