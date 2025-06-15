using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAGenerativa.Data.EF;

public class ResultadoAnalizadorOracioneRepository
{
    private readonly IagenerativaDbContext _dbContext;

    public ResultadoAnalizadorOracioneRepository(IagenerativaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ResultadoAnalizadorOracione>> GetAllAsync()
    {
        return await _dbContext.ResultadoAnalizadorOraciones.ToListAsync();
    }

    public async Task AddAsync(ResultadoAnalizadorOracione entity)
    {
        await _dbContext.ResultadoAnalizadorOraciones.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}