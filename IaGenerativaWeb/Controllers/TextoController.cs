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

        [HttpGet]
        public IActionResult Analizar()
        {
            return View(new TextoViewModel());
        }

        [HttpPost]
        public IActionResult Analizar(TextoViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Texto))
            {
                model.Clasificacion = _servicio.Clasificar(model.Texto);
            }
            return View(model);
        }

        public IActionResult AnalizarTexto(string texto)
        {
            var clasificador = new ClasificacionTextoService();
            var resultados = clasificador.ClasificarPartes(texto);

            // Cálculo de porcentajes
            int total = resultados.Count;
            int formales = resultados.Count(x => x.Etiqueta == "Formal");
            int informales = resultados.Count(x => x.Etiqueta == "Informal");

            double porcentajeFormal = total > 0 ? (formales * 100.0) / total : 0;
            double porcentajeInformal = total > 0 ? (informales * 100.0) / total : 0;

            // Armá un ViewBag para pasar los porcentajes
            ViewBag.PorcentajeFormal = porcentajeFormal;
            ViewBag.PorcentajeInformal = porcentajeInformal;
            ViewBag.Resultados = resultados;

            // Mostralo en la vista principal (Analizar)
            var model = new TextoViewModel();
            model.Texto = texto;
            model.ResultadosPartes = resultados;

            return View("Analizar", model);
        }


        [HttpPost]
        public IActionResult TransformarTexto(TextoViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Texto) && !string.IsNullOrWhiteSpace(model.OpcionTransformar))
            {
                if (model.OpcionTransformar == "Formal")
                {
                    model.TextoTransformado = model.Texto
                        .Replace("che", "estimado")
                        .Replace("te mando", "le envío")
                        .Replace("vos", "usted")
                        .Replace("gracias", "muchas gracias")
                        .Replace("dale", "de acuerdo");
                }
                else
                {
                    model.TextoTransformado = model.Texto
                        .Replace("estimado", "che")
                        .Replace("le envío", "te mando")
                        .Replace("usted", "vos")
                        .Replace("muchas gracias", "gracias")
                        .Replace("de acuerdo", "dale");
                }
            }
            return View("Analizar", model);
        }

    }
}
