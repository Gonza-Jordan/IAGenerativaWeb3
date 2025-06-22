Para probar: https://localhost:7007/Texto/Analizar

Luego agregar otro readme de presentacion.


Cambios en el modelo agregando interfaz y modificando el StartUp 

Me encargué de que el modelo de inteligencia artificial no tenga que entrenarse cada vez que se inicia la app. Para eso, implementé una lógica que guarda el modelo ya entrenado en un archivo `.zip`, y luego lo carga automáticamente al arrancar.

Metodos:
- `EntrenarYGuardarModelo(string rutaModelo)`  
  Entrena el modelo de clasificación de texto (formal/informal) usando los datos de la base y guarda ese modelo en un archivo `.zip`. Se usa para que el entrenamiento se haga una vez sola y no cada vez que arranca la app.

- `CargarModeloDesdeDisco(string rutaModelo)`
  Carga el modelo guardado en disco (el `.zip`) para poder usarlo sin necesidad de reentrenarlo. Se ejecuta automáticamente al inicio de la app.

- Ruta `/Admin/EntrenarModelo`  
  ES NECESARIO UNICAMENTE ejecutarlo cuando se agregan nuevas frases a la base de datos o se cambian clasificaciones. Esto vuelve a generar el `.zip` actualizado.

El archivo generado se llama:  
`wwwroot/modelos/modeloEntrenado.zip` 
Ahí queda guardado el "conocimiento" del modelo para poder usarlo sin entrenarlo cada vez.

