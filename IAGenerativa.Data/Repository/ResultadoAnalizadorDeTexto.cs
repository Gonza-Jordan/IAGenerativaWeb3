using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAGenerativa.Data.EF;

public class ResultadoAnalizadorDeTextoRepository
{
	private readonly IagenerativaDbContext _dbContext;

	public ResultadoAnalizadorDeTextoRepository(IagenerativaDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<List<ResultadoAnalizadorDeTexto>> GetAllAsync()
	{
		return await _dbContext.ResultadoAnalizadorDeTextos.ToListAsync();
	}

	public async Task AddAsync(ResultadoAnalizadorDeTexto entity)
	{
		await _dbContext.ResultadoAnalizadorDeTextos.AddAsync(entity);
		await _dbContext.SaveChangesAsync();
	}
}