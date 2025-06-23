// IStartupService.cs
using System.Threading.Tasks;
using IAGenerativa.Logica.Servicios.Interfaces;
using IAGenerativaDemo.Business.Servicios;

namespace IAGenerativaDemo.Business.Servicios
{
    public interface IStartupService
    {
        Task InitializeAsync();
    }
}

public class StartupService : IStartupService
{
    private readonly IModeloMLService _modeloMLService;
    
    public StartupService(IModeloMLService modeloMLService)
    {
        _modeloMLService = modeloMLService;
    }

 
    public Task InitializeAsync()
    {
        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "modelos", "modeloEntrenado.zip");
        _modeloMLService.CargarModeloDesdeDisco(ruta);
        return Task.CompletedTask;
    }

}