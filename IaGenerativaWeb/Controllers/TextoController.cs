using Microsoft.AspNetCore.Mvc;
using IAGenerativaDemo.Web.Models;
using IAGenerativaDemo.Business.Servicios;
using IAGenerativa.Logica.Servicios.Interfaces;
using IAGenerativa.Data.EF;

namespace IAGenerativaDemo.Web.Controllers
{
    public class TextoController : Controller
    {
        private readonly IClasificacionTextoService _servicio;


        public TextoController(IClasificacionTextoService servicio)
        {
            _servicio = servicio;
        }

        // HOME GENERAL (OPCIONAL)
        [HttpGet]
        public IActionResult Analizar()
        {
            return View(new TextoViewModel());
        }

        // ========== ANALIZADOR DE ORACIONES ==========
        [HttpGet]
        public IActionResult AnalizadorOraciones()
        {
            return View(new TextoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AnalizadorOraciones(TextoViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Texto))
            {
                model.Clasificacion = _servicio.Clasificar(model.Texto);

                Clasificacion clasificacionAguardar = await _servicio.ObtenerClasificacionPorNombre(model.Clasificacion);

                ResultadoAnalizadorOracione resultadoAnalizadorOraciones = new ResultadoAnalizadorOracione
                {
                    TextoOriginal = model.Texto,
                    ClasificacionId = clasificacionAguardar.Id,                    
                    FechaProcesamiento = DateTime.UtcNow
                };

                await _servicio.GuardarResultadoAnalizadorDeOraciones(resultadoAnalizadorOraciones);
            }
            return View("ResultadoAnalizadorOraciones", model);
        }

        // ========== ANALIZADOR DE TEXTOS ==========
        [HttpGet]
        public IActionResult AnalizadorTextos()
        {
            return View(new TextoViewModel());
        }

        [HttpPost]

        public async Task<IActionResult> AnalizadorTextos(TextoViewModel model)
        {           

            var resultados = _servicio.ClasificarPartes(model.Texto);

            int total = resultados.Count;
            int formales = resultados.Count(x => x.Etiqueta == "Formal");
            int informales = resultados.Count(x => x.Etiqueta == "Informal");

            double porcentajeFormal = total > 0 ? (formales * 100.0) / total : 0;
            double porcentajeInformal = total > 0 ? (informales * 100.0) / total : 0;

            string ambito = _servicio.DetectarAmbito(model.Texto);
            string estadoAnimo = await _servicio.DetectarEstadoAnimoAsync(model.Texto);

            model.ResultadosPartes = resultados;
            model.PorcentajeFormal = porcentajeFormal;
            model.PorcentajeInformal = porcentajeInformal;
            model.AmbitoSugerido = ambito;            
            model.ResultadoEstadoAnimo = estadoAnimo;

            TipoEstadoAnimo estadoAnimoAguardar = await _servicio.ObtenerTipoEstadoDeAnimoPorNombre(estadoAnimo);
            Ambito ambitoAguardar = await _servicio.ObtenerAmbitoPorNombre(ambito);

            ResultadoAnalizadorDeTexto resultadoAnalizador = new ResultadoAnalizadorDeTexto
            {
                TextoOriginal = model.Texto,
                PorcentajeFormal = porcentajeFormal,
                PorcentajeInformal = porcentajeInformal,
                AmbitoId = ambitoAguardar.Id,
                TipoEstadoAnimoId = estadoAnimoAguardar.Id,
                FechaProcesamiento = DateTime.UtcNow
            };

            await _servicio.GuardarResultadoAnalizadorDeTexto(resultadoAnalizador);

            return View("ResultadoAnalizadorTextos", model);
        }

        // ========== TRANSFORMADOR DE TEXTOS ==========
        [HttpGet]
        public IActionResult TransformadorTextos()
        {
            return View(new TextoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> TransformadorTextos(TextoViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Texto) && !string.IsNullOrWhiteSpace(model.OpcionTransformar))
            {
                model.TextoTransformado = _servicio.TransformarTexto(model.Texto, model.OpcionTransformar);
                model.AmbitoSugerido = _servicio.DetectarAmbito(model.TextoTransformado);

                Ambito ambitoAguardar = await _servicio.ObtenerAmbitoPorNombre(model.AmbitoSugerido);

                ResultadoTransformadorDeTexto resultadoTransformador = new ResultadoTransformadorDeTexto
                {
                    TextoOriginal = model.Texto,
                    TextoTransformado = model.TextoTransformado,
                    AmbitoId = ambitoAguardar.Id,
                    FechaProcesamiento = DateTime.UtcNow
                };
                await _servicio.GuardarResultadoTransformadorDeTexto(resultadoTransformador);
            }
            return View("ResultadoTransformadorTextos", model);
        }

        // ========== ANALIZADOR DE ESTADO DE ÁNIMO ==========
        [HttpGet]
        public IActionResult AnalizadorEstadoAnimo()
        {
            return View(new TextoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AnalizadorEstadoAnimo(TextoViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Texto))
            {
                model.ResultadoEstadoAnimo = await _servicio.DetectarEstadoAnimoAsync(model.Texto);

                TipoEstadoAnimo estadoAnimoAguardar = await _servicio.ObtenerTipoEstadoDeAnimoPorNombre(model.ResultadoEstadoAnimo);
                ResultadoAnalizadorEstadoAnimo resultadoAnalizadorEstadoAnimo = new ResultadoAnalizadorEstadoAnimo
                {
                    TextoOriginal = model.Texto,
                    TipoEstadoAnimoId = estadoAnimoAguardar.Id,
                    FechaProcesamiento = DateTime.UtcNow
                };
                await _servicio.GuardarResultadoAnalizadorDeEstAnimo(resultadoAnalizadorEstadoAnimo);
            }
            return View("ResultadoAnalizadorEstadoAnimo", model);
        }

    }
}
