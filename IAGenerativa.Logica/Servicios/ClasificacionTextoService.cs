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

        private PredictionEngine<TextoInput, TextoPrediccion> EntrenarModelo()
        {
            var datos = new List<TextoInput>
            {
                new TextoInput { Texto = "Estimado señor, le informo que su pedido fue procesado.", Etiqueta = "Formal" },
                new TextoInput { Texto = "Che, ya te mandé el archivo!", Etiqueta = "Informal" },
                new TextoInput { Texto = "Apreciado cliente, agradecemos su consulta.", Etiqueta = "Formal" },
                new TextoInput { Texto = "Dale, nos vemos!", Etiqueta = "Informal" },
            };

            var data = mlContext.Data.LoadFromEnumerable(datos);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("TextoFeaturizado", nameof(TextoInput.Texto))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("EtiquetaKey", nameof(TextoInput.Etiqueta)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("EtiquetaKey", "TextoFeaturizado"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("Prediccion", "PredictedLabel"));

            var model = pipeline.Fit(data);

            return mlContext.Model.CreatePredictionEngine<TextoInput, TextoPrediccion>(model);
        }

        public string Clasificar(string texto)
        {
            var resultado = predEngine.Predict(new TextoInput { Texto = texto });
            return resultado.Prediccion;
        }
    }

    public class TextoInput
    {
        public string Texto { get; set; }
        public string Etiqueta { get; set; }
    }

    public class TextoPrediccion
    {
        [ColumnName("Prediccion")]
        public string Prediccion { get; set; }
    }
}
