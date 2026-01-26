using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UiFilterField
{
    public int Id { get; set; }

    public int FilterId { get; set; }

    public string Key { get; set; }

    public string Label { get; set; }

    public string Type { get; set; }

    public string Placeholder { get; set; }

    public bool Disabled { get; set; }

    public string DateFormat { get; set; }

    public string OptionsSource { get; set; }

    public string OptionLabel { get; set; }

    public string OptionValue { get; set; }

    public bool? ShowClear { get; set; }

    public int? MaxLength { get; set; }

    public string ColClass { get; set; }

    public string DefaultValueJson { get; set; }

    public int Order { get; set; }

    public bool IsActive { get; set; }

    public virtual UiFilterConfig Filter { get; set; }
}
