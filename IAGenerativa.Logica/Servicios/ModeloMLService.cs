using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAGenerativa.Data.EF;
using IAGenerativa.Data.UnitOfWork;
using IAGenerativa.Logica.Servicios.Interfaces;
using IAGenerativaDemo.Business.Servicios;
using Microsoft.ML;

namespace IAGenerativa.Logica.Servicios
{
    public class ModeloMLService : IModeloMLService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MLContext _mlContext;
        private PredictionEngine<TextoInput, TextoPrediccion> _predEngine;

        public ModeloMLService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mlContext = new MLContext();
        }

        public void EntrenarYGuardarModelo(string rutaModelo)
        {
            var frases = _unitOfWork.GetRepository<FraseClasificacion>()
        .GetAllAsync("Clasificacion").Result.ToList();

            var datos = frases.Select(f => new TextoInput
            {
                Texto = f.Texto,
                Etiqueta = f.Clasificacion.Nombre
            }).ToList();

            var data = _mlContext.Data.LoadFromEnumerable(datos);

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("TextoFeaturizado", nameof(TextoInput.Texto))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("EtiquetaKey", nameof(TextoInput.Etiqueta)))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("EtiquetaKey", "TextoFeaturizado"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("Prediccion", "PredictedLabel"));

            var model = pipeline.Fit(data);

            using var fs = new FileStream(rutaModelo, FileMode.Create);
            _mlContext.Model.Save(model, data.Schema, fs);
        }

        public void CargarModeloDesdeDisco(string rutaModelo)
        {
            using var fs = new FileStream(rutaModelo, FileMode.Open);
            var model = _mlContext.Model.Load(fs, out _);
            _predEngine = _mlContext.Model.CreatePredictionEngine<TextoInput, TextoPrediccion>(model);
        }

        public string Clasificar(string texto)
        {
            return _predEngine?.Predict(new TextoInput { Texto = texto })?.Prediccion ?? "Desconocido";
        }
    }
}


