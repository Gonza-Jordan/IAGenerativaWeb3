using System;
using System.Collections.Generic;

namespace IAGenerativa.Data.EF;

public partial class ResultadoAnalizadorEstadoAnimo
{
    public int Id { get; set; }

    public string TextoOriginal { get; set; } = null!;

    public int TipoEstadoAnimoId { get; set; }

    public DateTime FechaProcesamiento { get; set; }

    public virtual TipoEstadoAnimo TipoEstadoAnimo { get; set; } = null!;
}
