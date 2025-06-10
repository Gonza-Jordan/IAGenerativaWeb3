using System;
using System.Collections.Generic;

namespace IAGenerativa.Data.EF;

public partial class Clasificacion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ResultadoAnalizadorDeTexto> ResultadoAnalizadorDeTextos { get; set; } = new List<ResultadoAnalizadorDeTexto>();

    public virtual ICollection<ResultadoAnalizadorOracione> ResultadoAnalizadorOraciones { get; set; } = new List<ResultadoAnalizadorOracione>();
}
