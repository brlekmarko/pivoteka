using Pivoteka.Commons;
using Pivoteka.DataAccess.PostgreSQL.Data;
using Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Pivoteka.Repositories.PostgreSQL;
public class NarucioPivoRepository : INarucioPivoRepository<int, NarucioPivo>
{
    private readonly PivotekaContext _dbContext;

    public NarucioPivoRepository(PivotekaContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool CheckAmount(IEnumerable<NarucioPivo> stavke)
    {
        foreach (var stavka in stavke)
        {
            var inventory = _dbContext.Set<Pivo>()
                .FirstOrDefault(p => p.Ime == stavka.ImePiva);

            if (inventory == null || inventory.Količina < stavka.Količina)
            {
                return false; // Not enough inventory
            }
        }
        return true; // Enough inventory for all items
    }

    public bool ReduceAmount(IEnumerable<NarucioPivo> stavke)
    {
        var isSuccess = false;
        foreach (var stavka in stavke)
        {
            var stanje = _dbContext.Set<Pivo>()
                .FirstOrDefault(p => p.Ime == stavka.ImePiva);
            if (stanje == null)
            {
                return false; // Item not found
            }
            if (stanje.Količina - stavka.Količina < 0)
            {
                return false; // Not enough inventory
            }
            stanje.Količina -= stavka.Količina;
            _dbContext.Entry(stanje).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            isSuccess = _dbContext.SaveChanges() > 0;
            
        }
        return isSuccess;
    }
}