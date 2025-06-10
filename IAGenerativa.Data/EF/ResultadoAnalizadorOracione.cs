using System;
using System.Collections.Generic;

namespace IAGenerativa.Data.EF;

public partial class ResultadoAnalizadorOracione
{
    public int Id { get; set; }

    public string TextoOriginal { get; set; } = null!;

    public int ClasificacionId { get; set; }

    public DateTime FechaProcesamiento { get; set; }

    public virtual Clasificacion Clasificacion { get; set; } = null!;
}
