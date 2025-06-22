using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAGenerativa.Logica.Servicios.Interfaces
{
    public interface IModeloMLService
    {
        void EntrenarYGuardarModelo(string rutaModelo);
        void CargarModeloDesdeDisco(string rutaModelo);
        string Clasificar(string texto);
    }
}
