using IAGenerativa.Data.EF;
using IAGenerativa.Data.Enums;
using IAGenerativa.Data.UnitOfWork;
using IAGenerativa.Logica.Servicios.Interfaces;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace IAGenerativaDemo.Business.Servicios
{
    public class ClasificacionTextoService : IClasificacionTextoService
    {
        private readonly MLContext mlContext;
        private readonly PredictionEngine<TextoInput, TextoPrediccion> predEngine;
        private readonly IUnitOfWork _unitOfWork;

        public ClasificacionTextoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            mlContext = new MLContext();
            predEngine = EntrenarModelo();
        }

        // ============ ML.NET Formal/Informal =============
        private PredictionEngine<TextoInput, TextoPrediccion> EntrenarModelo()
        {
            var frases = _unitOfWork.GetRepository<FraseClasificacion>()
                .GetAllAsync("Clasificacion").Result.ToList();


            var datos = frases.Select(f => new TextoInput
            {
                Texto = f.Texto,
                Etiqueta = f.Clasificacion.Nombre
            }).ToList();

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

        public List<(string Frase, string Etiqueta)> ClasificarPartes(string textoCompleto)
        {
            var oraciones = textoCompleto
                .Split(new[] { '.', '!', '?', ';', '\n' }, System.StringSplitOptions.RemoveEmptyEntries)
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

        public async Task<List<string>> DetectarAmbitosPorPalabrasAsync(string texto)
        {
            var palabras = texto
                .ToLower()
                .Split(new[] { ' ', '.', ',', ';', ':', '!', '?' }, System.StringSplitOptions.RemoveEmptyEntries);

            var palabrasClave = await _unitOfWork.GetRepository<PalabraClave>()
                .GetAllAsync();

            var relaciones = await _unitOfWork.GetRepository<PalabraClaveAmbito>()
                .GetAllAsync();

            var ambitos = await _unitOfWork.GetRepository<Ambito>()
                .GetAllAsync();

            var ambitosDetectados = new HashSet<string>();

            foreach (var palabra in palabras)
            {
                var pcs = palabrasClave.Where(pc => pc.Texto.ToLower() == palabra);

                foreach (var pc in pcs)
                {
                    var rels = relaciones.Where(r => r.PalabraClaveId == pc.Id);
                    foreach (var rel in rels)
                    {
                        var ambito = ambitos.FirstOrDefault(a => a.Id == rel.AmbitoId);
                        if (ambito != null)
                            ambitosDetectados.Add(ambito.Nombre);
                    }
                }
            }
            if (ambitosDetectados.Count == 0)
                ambitosDetectados.Add("General");

            return ambitosDetectados.ToList();
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

        public async Task<string> DetectarEstadoAnimoAsync(string texto)
        {
            texto = texto.ToLower();
            var estados = await _unitOfWork.GetRepository<EstadosAnimo>().GetAllAsync();

            if (estados.Any(e => e.TipoId == (int)TipoEstadoAnimoEnum.Positivo && texto.Contains(e.Nombre.ToLower())))
                return "Positivo";
            if (estados.Any(e => e.TipoId == (int)TipoEstadoAnimoEnum.Negativo && texto.Contains(e.Nombre.ToLower())))
                return "Negativo";
            return "Neutro";
        }

        public async Task<string> TransformarTexto(string textoOriginal, string tonoDestino)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "token");

            string instruccion = tonoDestino.ToLower() switch
            {
                "formal" => $"Reescribí este texto en español con un tono formal: \"{textoOriginal}\". Devuelve UNICAMENTE el texto transformado",
                "informal" => $"Reescribí este texto en español con un tono informal: \"{textoOriginal}\". Devuelve UNICAMENTE el texto transformado",
                _ => $"Parafraseá este texto en español: \"{textoOriginal}\" . Devuelve UNICAMENTE el texto transformado"
            };

            var requestBody = new
            {
                model = "deepseek-ai/DeepSeek-R1-fast",
                messages = new[]
                {
            new { role = "user", content = instruccion }
        },
                stream = false
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://router.huggingface.co/nebius/v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"[Error {response.StatusCode}] {error}";
            }

            var json = await response.Content.ReadAsStringAsync();

            try
            {
                var doc = JsonDocument.Parse(json);
                var resultadoCompleto = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                string etiquetaCierre = "</think>";
                int indice = resultadoCompleto.LastIndexOf(etiquetaCierre);

                var resultado = resultadoCompleto.Substring(indice + etiquetaCierre.Length).Trim();

                return resultado?.Trim() ?? "[Respuesta vacía]";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar JSON: {ex.Message}");
                return "[Error] No se pudo interpretar la respuesta del modelo.";
            }
        }

        public async Task<TipoEstadoAnimo> ObtenerTipoEstadoDeAnimoPorNombre(string nombre)
        {
            nombre = nombre.ToLower();
            return await _unitOfWork.GetRepository<TipoEstadoAnimo>().GetOne(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }

        public async Task<Ambito> ObtenerAmbitoPorNombre(string nombre)
        {
            nombre = nombre.ToLower();
            return await _unitOfWork.GetRepository<Ambito>().GetOne(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }

        public async Task<Clasificacion> ObtenerClasificacionPorNombre(string nombre)
        {
            nombre = nombre.ToLower();
            return await _unitOfWork.GetRepository<Clasificacion>().GetOne(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }

        public async Task GuardarResultadoAnalizadorDeTexto(ResultadoAnalizadorDeTexto resultadoAnalizadorDeTexto)
        {
            await _unitOfWork.GetRepository<ResultadoAnalizadorDeTexto>().AddAsync(resultadoAnalizadorDeTexto);
            await _unitOfWork.SaveAsync();
        }

        public async Task GuardarResultadoTransformadorDeTexto(ResultadoTransformadorDeTexto transformadorDeTexto)
        {
            await _unitOfWork.GetRepository<ResultadoTransformadorDeTexto>().AddAsync(transformadorDeTexto);
            await _unitOfWork.SaveAsync();
        }

        public async Task GuardarResultadoAnalizadorDeOraciones(ResultadoAnalizadorOracione resAnalizadorDeOraciones)
        {
            await _unitOfWork.GetRepository<ResultadoAnalizadorOracione>().AddAsync(resAnalizadorDeOraciones);
            await _unitOfWork.SaveAsync();
        }

        public async Task GuardarResultadoAnalizadorDeEstAnimo(ResultadoAnalizadorEstadoAnimo resAnalizadorEstadoAnimo)
        {
            await _unitOfWork.GetRepository<ResultadoAnalizadorEstadoAnimo>().AddAsync(resAnalizadorEstadoAnimo);
            await _unitOfWork.SaveAsync();
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
