using System;
using System.Collections.Generic;

namespace ASA.Models;

public partial class CatCategoria
{
    public int IdCategoria { get; set; }

    public string Categoria { get; set; } = null!;

    public int? IdSubcategoria { get; set; }

    public virtual ICollection<CatLibro> CatLibros { get; set; } = new List<CatLibro>();

    public virtual CatSubcategoria? IdSubcategoriaNavigation { get; set; }
}
