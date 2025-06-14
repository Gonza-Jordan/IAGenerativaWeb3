using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAGenerativa.Data.EF;

public class ResultadoAnalizadorEstadoAnimoRepository
{
    private readonly IagenerativaDbContext _dbContext;

    public ResultadoAnalizadorEstadoAnimoRepository(IagenerativaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ResultadoAnalizadorEstadoAnimo>> GetAllAsync()
    {
        return await _dbContext.ResultadoAnalizadorEstadoAnimos.ToListAsync();
    }

    public async Task AddAsync(ResultadoAnalizadorEstadoAnimo entity)
    {
        await _dbContext.ResultadoAnalizadorEstadoAnimos.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}