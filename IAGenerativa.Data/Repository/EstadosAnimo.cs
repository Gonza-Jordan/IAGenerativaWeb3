using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAGenerativa.Data.EF;

public class EstadosAnimoRepository
{
    private readonly IagenerativaDbContext _dbContext;

    public EstadosAnimoRepository(IagenerativaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<EstadosAnimo>> GetAllAsync()
    {
        return await _dbContext.EstadosAnimos.ToListAsync();
    }

    public async Task<EstadosAnimo> GetByNombreAsync(string nombre)
    {
        return await _dbContext.EstadosAnimos.FirstOrDefaultAsync(e => e.Nombre.ToLower() == nombre.ToLower());
    }
}