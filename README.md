# 📚 IA Generativa y Clasificación de Texto en .NET

Este proyecto combina **ML.NET** para clasificación rápida de texto dentro del ecosistema .NET con **Hugging Face** para generación avanzada de lenguaje natural. Ideal para aplicaciones donde se requiere inferencia local eficiente y generación de texto contextual desde un modelo de IA preentrenado.


## 🚀 Tecnologías utilizadas

- ✅ **.NET 8**
- 🧠 **ML.NET** – Para entrenar y ejecutar modelos de clasificación textual.
- 🤖 **Hugging Face API** – Generación de texto usando modelos como GPT-2.
- 🗃️ **Entity Framework Core** – Acceso a datos con patrón Repository y Unit of Work.
- 💾 **SQL Server** – Almacenamiento de frases y resultados.


## 📦 Estructura del proyecto
/IAGenerativaWeb
│
├── IAGenerativa.Data           -> DbContext, entidades y repositorios, UnitOfWork
├── IAGenerativa.Business       -> Servicios de ML.NET, Hugging Face, 
├── IAGenerativa.Web            -> ASP.NET MVC (UI, Controllers)
├── appsettings.json            -> Configuración de API y conexión DB
└── README.md


Funcionalidades principales
•	Clasificación automática de texto como Formal o Informal usando ML.NET.
•	Generación de nuevo contenido a partir de un prompt con Hugging Face (modelo GPT-2).
•	Persistencia de resultados en SQL Server.
•	Entrenamiento y carga del modelo ML.NET en tiempo de ejecución.


⚙️ Configuración inicial
1 - Cloná el repositorio:
git clone https://github.com/Gonza-Jordan/IAGenerativaWeb3.git

2 - Configuración de appsettings.json
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=IAGenerativaDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "IAGenerativeModel": {
    "AccessToken": "token",
    "UrlBaseModel": "https://router.huggingface.co/nebius/v1/chat/completions",
    "ModelName": "deepseek-ai/DeepSeek-R1-fast"
  }
3 – Ejecutar los Scripts en el orden indicado en su nombre que se encuentra dentro de la carpeta de Scripts del proyecto IAGenerativa.Data
4 – Correr la aplicación con dotnet run --project IaGenerativaWeb/IaGenerativaWeb.csproj
5. Ver la salida
La consola te mostrará algo como:
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
6 – Abrir el navegador en la url indicada en el paso 5.


🔍 Cómo funciona
•	ML.NET:
o	Se entrena un modelo en base a frases etiquetadas desde la base de datos.
o	Se reutiliza en el PredictionEngine.
o	En una próxima versión va a poder regenerarse si se agregan más datos.
•	Hugging Face API:
o	Envia un prompt al endpoint REST y devuelve texto generado.
o	Se usa un token Bearer gratuito.

📈 Futuras mejoras
•	Exportar/visualizar estadísticas de clasificación.
•	Reentrenamiento automático basado en frases nuevas.


