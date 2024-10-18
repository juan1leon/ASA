using System;
using System.Collections.Generic;

namespace ASA.Models;

public partial class CatLibro
{
    public int IdLibro { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdCategoria { get; set; }

    public int IdEstado { get; set; }

    public virtual CatCategoria? IdCategoriaNavigation { get; set; }

    public virtual CatEstado? IdEstadoNavigation { get; set; }
}
