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
            var resultados = _servicio.ClasificarPartes(texto);
            var (porcentajeFormal, porcentajeInformal) = _servicio.CalcularPorcentajeFormalInformal(texto);

            ViewBag.PorcentajeFormal = porcentajeFormal;
            ViewBag.PorcentajeInformal = porcentajeInformal;

            return View("Analisis", resultados);
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
