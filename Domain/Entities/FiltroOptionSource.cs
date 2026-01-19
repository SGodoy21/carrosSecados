using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class FiltroOptionSource
{
    public string Code { get; set; }

    public string SourceType { get; set; }

    public string SqlQuery { get; set; }

    public bool Activo { get; set; }

    public int CacheSeconds { get; set; }
}
