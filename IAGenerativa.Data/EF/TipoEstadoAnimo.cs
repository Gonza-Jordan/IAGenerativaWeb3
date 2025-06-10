using System;
using System.Collections.Generic;

namespace IAGenerativa.Data.EF;

public partial class TipoEstadoAnimo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<EstadosAnimo> EstadosAnimos { get; set; } = new List<EstadosAnimo>();

    public virtual ICollection<ResultadoAnalizadorDeTexto> ResultadoAnalizadorDeTextos { get; set; } = new List<ResultadoAnalizadorDeTexto>();

    public virtual ICollection<ResultadoAnalizadorEstadoAnimo> ResultadoAnalizadorEstadoAnimos { get; set; } = new List<ResultadoAnalizadorEstadoAnimo>();
}
