using System.ComponentModel.DataAnnotations;

namespace IAGenerativaDemo.Web.Models
{
    public class TextoViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un texto valido.")]
        public string Texto { get; set; }
        public string Clasificacion { get; set; }
        [Required(ErrorMessage = "Debe seleccionar entre formal e informal.")]
        public string OpcionTransformar { get; set; }
        public string TextoTransformado { get; set; }
        public double PorcentajeFormal { get; set; }
        public double PorcentajeInformal { get; set; }
        public string AmbitoSugerido { get; set; }
        public List<(string Frase, string Etiqueta)> ResultadosPartes { get; set; }
        public string ResultadoEstadoAnimo { get; set; }

    }
}
