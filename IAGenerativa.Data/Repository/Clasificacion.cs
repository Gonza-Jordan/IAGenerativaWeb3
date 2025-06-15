using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAGenerativa.Data.EF;

public class ClasificacionRepository
{
    private readonly IagenerativaDbContext _dbContext;

    public ClasificacionRepository(IagenerativaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Clasificacion>> GetAllAsync()
    {
        return await _dbContext.Clasificacions.ToListAsync();
    }

    public async Task<Clasificacion> GetByNombreAsync(string nombre)
    {
        return await _dbContext.Clasificacions.FirstOrDefaultAsync(c => c.Nombre.ToLower() == nombre.ToLower());
    }
}