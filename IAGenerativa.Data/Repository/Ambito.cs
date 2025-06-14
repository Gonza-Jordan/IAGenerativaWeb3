using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAGenerativa.Data.EF;

public class AmbitoRepository
{
    private readonly IagenerativaDbContext _dbContext;

    public AmbitoRepository(IagenerativaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Ambito>> GetAllAsync()
    {
        return await _dbContext.Ambitos.ToListAsync();
    }

    public async Task<Ambito> GetByNombreAsync(string nombre)
    {
        return await _dbContext.Ambitos.FirstOrDefaultAsync(a => a.Nombre.ToLower() == nombre.ToLower());
    }
}