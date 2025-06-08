// IStartupService.cs
using System.Threading.Tasks;
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
    private readonly ClasificacionTextoService _clasificacionService;

    public StartupService(ClasificacionTextoService clasificacionService)
    {
        _clasificacionService = clasificacionService;
    }

    public Task InitializeAsync()
    {
        var testResult = _clasificacionService.Clasificar("Test de inicialización");
        return Task.CompletedTask;
    }
}