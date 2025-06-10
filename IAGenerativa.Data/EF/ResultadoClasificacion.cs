using System;
using System.Collections.Generic;

namespace IAGenerativa.Data.EF;

public partial class ResultadoClasificacion
{
    public int Id { get; set; }

    public string TextoOriginal { get; set; } = null!;

    public int? ClasificacionId { get; set; }

    public int? AmbitoId { get; set; }

    public int? EstadoAnimoId { get; set; }

    public DateTime FechaProcesamiento { get; set; }

    public double PorcentajeFormal { get; set; }

    public double PorcentajeInformal { get; set; }

    public virtual Ambito? Ambito { get; set; }

    public virtual Clasificacion? Clasificacion { get; set; }

    public virtual EstadosAnimo? EstadoAnimo { get; set; }
}
