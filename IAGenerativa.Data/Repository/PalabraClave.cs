using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAGenerativa.Data.EF;


public class PalabraClaveRepository
{
    private readonly IagenerativaDbContext _dbContext;

    public PalabraClaveRepository(IagenerativaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PalabraClave>> GetPalabrasClaveConAmbitosAsync()
    {
        return await _dbContext.PalabrasClave
            .Include(pc => pc.PalabraClaveAmbitos)
            .ThenInclude(rel => rel.Ambito)
            .ToListAsync();
    }
}
