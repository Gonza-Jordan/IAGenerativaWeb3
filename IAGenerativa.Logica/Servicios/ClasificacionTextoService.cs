using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;

namespace IAGenerativaDemo.Business.Servicios
{
    public class ClasificacionTextoService
    {
        private readonly MLContext mlContext;
        private readonly PredictionEngine<TextoInput, TextoPrediccion> predEngine;

        public ClasificacionTextoService()
        {
            mlContext = new MLContext();
            predEngine = EntrenarModelo();
        }
        //Metodo para cargar oraciones formales o informales
        private PredictionEngine<TextoInput, TextoPrediccion> EntrenarModelo()
        {
            var datos = new List<TextoInput>
            {
                // Formales
                new TextoInput { Texto = "Estimado señor, le informo que su pedido fue procesado.", Etiqueta = "Formal" },
                new TextoInput { Texto = "Apreciado cliente, agradecemos su consulta.", Etiqueta = "Formal" },
                new TextoInput { Texto = "Por favor, envíe la documentación solicitada.", Etiqueta = "Formal" },
                new TextoInput { Texto = "Nos dirigimos a usted a fin de comunicarle...", Etiqueta = "Formal" },
                new TextoInput { Texto = "Le saludamos atentamente.", Etiqueta = "Formal" },
                new TextoInput { Texto = "Quedamos a su disposición para cualquier consulta.", Etiqueta = "Formal" },
                new TextoInput { Texto = "Sr. Juan, agradecemos su pronta respuesta.", Etiqueta = "Formal" },
                new TextoInput { Texto = "Agradecemos su atención.", Etiqueta = "Formal" },

                // Informales
                new TextoInput { Texto = "Che, ya te mandé el archivo!", Etiqueta = "Informal" },
                new TextoInput { Texto = "Dale, nos vemos!", Etiqueta = "Informal" },
                new TextoInput { Texto = "Mandame el archivo cuando puedas.", Etiqueta = "Informal" },
                new TextoInput { Texto = "Pasame la data así lo hago.", Etiqueta = "Informal" },
                new TextoInput { Texto = "Hola, qué hacés?", Etiqueta = "Informal" },
                new TextoInput { Texto = "Fijate si podés.", Etiqueta = "Informal" },
                new TextoInput { Texto = "Te mando un saludo!", Etiqueta = "Informal" },
                new TextoInput { Texto = "Nos vemos más tarde.", Etiqueta = "Informal" },
            };


            var data = mlContext.Data.LoadFromEnumerable(datos);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("TextoFeaturizado", nameof(TextoInput.Texto))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("EtiquetaKey", nameof(TextoInput.Etiqueta)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("EtiquetaKey", "TextoFeaturizado"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("Prediccion", "PredictedLabel"));

            var model = pipeline.Fit(data);

            return mlContext.Model.CreatePredictionEngine<TextoInput, TextoPrediccion>(model);
        }
        //Metodo que clasifica segun la etiqueta de la oracion.
        public string Clasificar(string texto)
        {
            var resultado = predEngine.Predict(new TextoInput { Texto = texto });
            return resultado.Prediccion;
        }
        public List<(string Frase, string Etiqueta)> ClasificarPartes(string textoCompleto)
        {
            // Separá en oraciones.
            var oraciones = textoCompleto
                .Split(new[] { '.', '!', '?', ';', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(o => o.Trim())
                .Where(o => !string.IsNullOrEmpty(o))
                .ToList();

            var resultados = new List<(string Frase, string Etiqueta)>();
            foreach (var oracion in oraciones)
            {
                var etiqueta = Clasificar(oracion);
                resultados.Add((oracion, etiqueta));
            }
            return resultados;
        }
        //Metodo para calcular porcentajes
        public (double PorcentajeFormal, double PorcentajeInformal) CalcularPorcentajeFormalInformal(string textoCompleto)
        {
            var partes = ClasificarPartes(textoCompleto);

            int total = partes.Count;
            if (total == 0) return (0, 0);

            int formales = partes.Count(p => p.Etiqueta == "Formal");
            int informales = partes.Count(p => p.Etiqueta == "Informal");

            double porcentajeFormal = (formales * 100.0) / total;
            double porcentajeInformal = (informales * 100.0) / total;

            return (porcentajeFormal, porcentajeInformal);
        }
        public string DetectarAmbito(string texto)
        {
            texto = texto.ToLower();

            if (texto.Contains("estimado") || texto.Contains("le informo") || texto.Contains("atentamente") || texto.Contains("solicitud") || texto.Contains("documentación") || texto.Contains("comunicamos"))
                return "Laboral/Profesional";
            if (texto.Contains("profesor") || texto.Contains("tarea") || texto.Contains("clase") || texto.Contains("examen") || texto.Contains("universidad") || texto.Contains("colegio"))
                return "Educativo";
            if (texto.Contains("mamá") || texto.Contains("papá") || texto.Contains("familia") || texto.Contains("hermano") || texto.Contains("abuelo") || texto.Contains("cena"))
                return "Familiar";
            if (texto.Contains("che") || texto.Contains("dale") || texto.Contains("nos vemos") || texto.Contains("fiesta") || texto.Contains("amigo") || texto.Contains("juntada"))
                return "Amistoso";
            if (texto.Contains("cliente") || texto.Contains("compra") || texto.Contains("venta") || texto.Contains("pedido") || texto.Contains("factura") || texto.Contains("presupuesto"))
                return "Comercial";
            return "General";
        }
        public string TransformarTexto(string texto, string opcion)
        {
            if (opcion == "Formal")
            {
                // Ejemplo de transformación: simplemente hace el texto en mayúsculas y agrega formalidad.
                return "Estimado/a: " + texto.ToUpper();
            }
            else
            {
                // Ejemplo de transformación: lo hace en minúsculas y lo simplifica.
                return "Che: " + texto.ToLower();
            }
        }
        public string DetectarEstadoAnimo(string texto)
        {
            var positivo = new[] { "feliz", "alegre", "genial", "excelente", "fantástico", "bien", "contento" };
            var negativo = new[] { "triste", "mal", "deprimido", "cansado", "horrible", "enojado" };

            texto = texto.ToLower();

            if (positivo.Any(p => texto.Contains(p)))
                return "Positivo";
            if (negativo.Any(n => texto.Contains(n)))
                return "Negativo";
            return "Neutro";
        }

    }
    public class TextoInput
    {
        public required string Texto { get; set; }
        public string Etiqueta { get; set; }
    }

    public class TextoPrediccion
    {
        [ColumnName("Prediccion")]
        public string Prediccion { get; set; }
    }

}
