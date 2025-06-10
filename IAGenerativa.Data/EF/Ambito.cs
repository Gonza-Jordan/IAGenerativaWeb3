using System;
using System.Collections.Generic;

namespace IAGenerativa.Data.EF;

public partial class Ambito
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ResultadoAnalizadorDeTexto> ResultadoAnalizadorDeTextos { get; set; } = new List<ResultadoAnalizadorDeTexto>();

    public virtual ICollection<ResultadoTransformadorDeTexto> ResultadoTransformadorDeTextos { get; set; } = new List<ResultadoTransformadorDeTexto>();
    public virtual ICollection<PalabraClaveAmbito> PalabraClaveAmbitos { get; set; }
}
