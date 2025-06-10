using System;
using System.Collections.Generic;

namespace IAGenerativa.Data.EF;

public partial class ResultadoTransformadorDeTexto
{
    public int Id { get; set; }

    public string TextoOriginal { get; set; } = null!;

    public string? TextoTransformado { get; set; }

    public int AmbitoId { get; set; }

    public DateTime FechaProcesamiento { get; set; }

    public virtual Ambito Ambito { get; set; } = null!;
}
