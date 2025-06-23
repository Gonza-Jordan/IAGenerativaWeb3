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
    private readonly IClasificacionTextoService _clasificacionService;

    public StartupService(IClasificacionTextoService clasificacionService) 
    {
        _clasificacionService = clasificacionService;
    }


    public Task InitializeAsync()
    {
        _clasificacionService.Clasificar("Test de inicialización");
        return Task.CompletedTask;
    }
}