using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAGenerativa.Data.EF;

public class PalabraClaveAmbitoRepository
{
    private readonly IagenerativaDbContext _dbContext;

    public PalabraClaveAmbitoRepository(IagenerativaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PalabraClaveAmbito>> GetAllAsync()
    {
        return await _dbContext.PalabraClaveAmbitos
            .Include(pca => pca.PalabraClave)
            .Include(pca => pca.Ambito)
            .ToListAsync();
    }

    public async Task<List<PalabraClaveAmbito>> GetByPalabraClaveIdAsync(int palabraClaveId)
    {
        return await _dbContext.PalabraClaveAmbitos
            .Where(pca => pca.PalabraClaveId == palabraClaveId)
            .Include(pca => pca.Ambito)
            .ToListAsync();
    }

    public async Task<List<PalabraClaveAmbito>> GetByAmbitoIdAsync(int ambitoId)
    {
        return await _dbContext.PalabraClaveAmbitos
            .Where(pca => pca.AmbitoId == ambitoId)
            .Include(pca => pca.PalabraClave)
            .ToListAsync();
    }
}