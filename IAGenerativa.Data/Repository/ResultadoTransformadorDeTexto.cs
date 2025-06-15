using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAGenerativa.Data.EF;

public class ResultadoTransformadorDeTextoRepository
{
    private readonly IagenerativaDbContext _dbContext;

    public ResultadoTransformadorDeTextoRepository(IagenerativaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ResultadoTransformadorDeTexto>> GetAllAsync()
    {
        return await _dbContext.ResultadoTransformadorDeTextos.ToListAsync();
    }

    public async Task AddAsync(ResultadoTransformadorDeTexto entity)
    {
        await _dbContext.ResultadoTransformadorDeTextos.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}