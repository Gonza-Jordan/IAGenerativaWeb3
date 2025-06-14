using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAGenerativa.Data.EF;

public class TipoEstadoAnimoRepository
{
    private readonly IagenerativaDbContext _dbContext;

    public TipoEstadoAnimoRepository(IagenerativaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TipoEstadoAnimo>> GetAllAsync()
    {
        return await _dbContext.TipoEstadoAnimos.ToListAsync();
    }

    public async Task<TipoEstadoAnimo> GetByNombreAsync(string nombre)
    {
        return await _dbContext.TipoEstadoAnimos.FirstOrDefaultAsync(t => t.Nombre.ToLower() == nombre.ToLower());
    }
}