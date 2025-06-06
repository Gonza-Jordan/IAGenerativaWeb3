using Microsoft.AspNetCore.Mvc;
using IAGenerativaDemo.Web.Models;
using IAGenerativaDemo.Business.Servicios;

namespace IAGenerativaDemo.Web.Controllers
{
    public class TextoController : Controller
    {
        private readonly ClasificacionTextoService _servicio;

        public TextoController()
        {
            _servicio = new ClasificacionTextoService();
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
        public IActionResult AnalizadorOraciones(TextoViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Texto))
            {
                model.Clasificacion = _servicio.Clasificar(model.Texto);
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
        public IActionResult AnalizadorTextos(TextoViewModel model)
        {
            var clasificador = new ClasificacionTextoService();
            var resultados = clasificador.ClasificarPartes(model.Texto);

            int total = resultados.Count;
            int formales = resultados.Count(x => x.Etiqueta == "Formal");
            int informales = resultados.Count(x => x.Etiqueta == "Informal");

            double porcentajeFormal = total > 0 ? (formales * 100.0) / total : 0;
            double porcentajeInformal = total > 0 ? (informales * 100.0) / total : 0;

            string ambito = clasificador.DetectarAmbito(model.Texto);
            string estadoAnimo = clasificador.DetectarEstadoAnimo(model.Texto);

            model.ResultadosPartes = resultados;
            model.PorcentajeFormal = porcentajeFormal;
            model.PorcentajeInformal = porcentajeInformal;
            model.AmbitoSugerido = ambito;
            model.ResultadoEstadoAnimo = estadoAnimo;

            return View("ResultadoAnalizadorTextos", model);
        }

        // ========== TRANSFORMADOR DE TEXTOS ==========
        [HttpGet]
        public IActionResult TransformadorTextos()
        {
            return View(new TextoViewModel());
        }

        [HttpPost]
        public IActionResult TransformadorTextos(TextoViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Texto) && !string.IsNullOrWhiteSpace(model.OpcionTransformar))
            {
                model.TextoTransformado = _servicio.TransformarTexto(model.Texto, model.OpcionTransformar);
                model.AmbitoSugerido = _servicio.DetectarAmbito(model.TextoTransformado);
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
        public IActionResult AnalizadorEstadoAnimo(TextoViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Texto))
            {
                model.ResultadoEstadoAnimo = _servicio.DetectarEstadoAnimo(model.Texto);
            }
            return View("ResultadoAnalizadorEstadoAnimo", model);
        }

    }
}
