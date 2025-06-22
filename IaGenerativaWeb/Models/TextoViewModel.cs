namespace IAGenerativaDemo.Web.Models
{
    public class TextoViewModel
    {
        public string Texto { get; set; }
        public string Clasificacion { get; set; }
        public string OpcionTransformar { get; set; }
        public string TextoTransformado { get; set; }
        public double PorcentajeFormal { get; set; }
        public double PorcentajeInformal { get; set; }
        public string AmbitoSugerido { get; set; }
        public List<(string Frase, string Etiqueta)> ResultadosPartes { get; set; }
        public string ResultadoEstadoAnimo { get; set; }

    }
}
