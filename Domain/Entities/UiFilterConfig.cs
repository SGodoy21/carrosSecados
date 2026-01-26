using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UiFilterConfig
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Title { get; set; }

    public bool ShowClearButton { get; set; }

    public bool ShowApplyButton { get; set; }

    public string ClearLabel { get; set; }

    public string ApplyLabel { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<UiFilterField> UisFiltersFields { get; set; } = new List<UiFilterField>();
}
