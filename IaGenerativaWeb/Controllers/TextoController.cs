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
            return View("Analisis", resultados);
        }

    }
}
