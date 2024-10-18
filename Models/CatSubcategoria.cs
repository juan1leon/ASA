using System;
using System.Collections.Generic;

namespace ASA.Models;

public partial class CatSubcategoria
{
    public int IdSubcategoria { get; set; }

    public string Subcategoria { get; set; } = null!;

    public virtual ICollection<CatCategoria> CatCategoria { get; set; } = new List<CatCategoria>();
}
