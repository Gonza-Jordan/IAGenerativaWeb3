using IAGenerativa.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAGenerativa.Logica.Servicios.Interfaces
{
    public interface IClasificacionTextoService
    {
        string Clasificar(string texto);
        List<(string Frase, string Etiqueta)> ClasificarPartes(string textoCompleto);
        (double PorcentajeFormal, double PorcentajeInformal) CalcularPorcentajeFormalInformal(string textoCompleto);
        string DetectarAmbito(string texto);
        string TransformarTexto(string texto, string opcion);
        Task<string> DetectarEstadoAnimoAsync(string texto);
        Task<TipoEstadoAnimo> ObtenerTipoEstadoDeAnimoPorNombre(string nombre);
        Task<Ambito> ObtenerAmbitoPorNombre(string nombre);
        Task<Clasificacion> ObtenerClasificacionPorNombre(string nombre);
        Task GuardarResultadoAnalizadorDeTexto(ResultadoAnalizadorDeTexto resultadoAnalizadorDeTexto);
        Task GuardarResultadoTransformadorDeTexto(ResultadoTransformadorDeTexto transformadorDeTexto);
        Task GuardarResultadoAnalizadorDeOraciones(ResultadoAnalizadorOracione resAnalizadorDeOraciones);
        Task GuardarResultadoAnalizadorDeEstAnimo(ResultadoAnalizadorEstadoAnimo resAnalizadorEstadoAnimo);
    }
}
