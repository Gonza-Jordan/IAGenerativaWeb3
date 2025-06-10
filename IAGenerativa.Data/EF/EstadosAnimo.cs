using System;
using System.Collections.Generic;

namespace IAGenerativa.Data.EF;

public partial class EstadosAnimo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int TipoId { get; set; }

    public virtual TipoEstadoAnimo Tipo { get; set; } = null!;
}
