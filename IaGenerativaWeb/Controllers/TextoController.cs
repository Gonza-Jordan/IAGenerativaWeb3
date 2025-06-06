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
        public IActionResult AnalizadorTextos(string texto)
        {
            var clasificador = new ClasificacionTextoService();
            var resultados = clasificador.ClasificarPartes(texto);

            // Calculá los porcentajes
            int total = resultados.Count;
            int formales = resultados.Count(x => x.Etiqueta == "Formal");
            int informales = resultados.Count(x => x.Etiqueta == "Informal");

            double porcentajeFormal = total > 0 ? (formales * 100.0) / total : 0;
            double porcentajeInformal = total > 0 ? (informales * 100.0) / total : 0;

            // Detectá el ámbito sugerido (si tenés el método)
            string ambito = clasificador.DetectarAmbito(texto);

            var model = new TextoViewModel
            {
                ResultadosPartes = resultados,
                PorcentajeFormal = porcentajeFormal,
                PorcentajeInformal = porcentajeInformal,
                AmbitoSugerido = ambito
            };

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
    }
}
