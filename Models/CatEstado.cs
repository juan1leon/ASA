using System;
using System.Collections.Generic;

namespace ASA.Models;

public partial class CatEstado
{
    public int IdEstado { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<CatLibro> CatLibros { get; set; } = new List<CatLibro>();
}
