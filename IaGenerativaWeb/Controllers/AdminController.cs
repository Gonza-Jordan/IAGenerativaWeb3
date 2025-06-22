using IAGenerativa.Logica.Servicios.Interfaces;
using IAGenerativaDemo.Business.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace IAGenerativaWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IModeloMLService _modeloServicio;

        public AdminController(IModeloMLService modeloServicio)
        {
            _modeloServicio = modeloServicio;
        }

        public IActionResult EntrenarModelo()
        {
            var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "modelos", "modeloEntrenado.zip");
            _modeloServicio.EntrenarYGuardarModelo(ruta);
            return Content("Modelo entrenado y guardado correctamente.");
        }
    }

}
