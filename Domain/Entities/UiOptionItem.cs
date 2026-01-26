using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UiOptionItem
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string Label { get; set; }

    public string Value { get; set; }

    public int Order { get; set; }

    public bool IsActive { get; set; }

    public virtual UiOptionList List { get; set; }
}
