    using Pivoteka.Commons;
using Pivoteka.DataAccess.PostgreSQL.Data;
using Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Pivoteka.Repositories.PostgreSQL;
public class PivoRepository : IPivoRepository<string, Pivo>
{
    private readonly PivotekaContext _dbContext;

    public PivoRepository(PivotekaContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Pivo model)
    {
        return _dbContext.Pivo
                         .AsNoTracking()
                         .Contains(model);
    }

    public bool Exists(string ime)
    {
        var model = _dbContext.Pivo
                              .AsNoTracking()
                              .FirstOrDefault(pivo => pivo.Ime.Equals(ime));
        return model is not null;
    }

    public Option<Pivo> Get(string ime)
    {
        var model = _dbContext.Pivo
                              .AsNoTracking()
                              .FirstOrDefault(pivo => pivo.Ime.Equals(ime));

        return model is not null
            ? Options.Some(model)
            : Options.None<Pivo>();
    }

    //public Option<Pivo> GetAggregate(string ime)
    //{
    //    var model = _dbContext.Pivo
    //                          .Include(pivo => pivo.PivoRoles)
    //                          .ThenInclude(pivoRoles => pivoRoles.Role)
    //                          .AsNoTracking()
    //                          .FirstOrDefault(pivo => pivo.Ime.Equals(ime)); // give me the first or null; substitute for .Where()
    //                                                                           // single or default throws an exception if more than one element meets the criteria

    //    return model is not null
    //        ? Options.Some(model)
    //        : Options.None<Pivo>();
    //}

    public IEnumerable<Pivo> GetAll()
    {
        var models = _dbContext.Pivo
                               .ToList();

        return models;
    }

    //public IEnumerable<Pivo> GetAllAggregates()
    //{
    //    var models = _dbContext.Pivo
    //                           .Include(pivo => pivo.PivoRoles)
    //                           .ThenInclude(pivoRoles => pivoRoles.Role)
    //                           .ToList();

    //    return models;
    //}

    public bool Insert(Pivo model)
    {
        if (_dbContext.Pivo.Add(model).State == Microsoft.EntityFrameworkCore.EntityState.Added)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Add attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

    public bool Remove(string ime)
    {
        var model = _dbContext.Pivo
                              .AsNoTracking()
                              .FirstOrDefault(pivo => pivo.Ime.Equals(ime));

        if (model is not null)
        {
            _dbContext.Pivo.Remove(model);

            return _dbContext.SaveChanges() > 0;
        }
        return false;
    }

    public bool Update(Pivo model)
    {
        // detach
        if (_dbContext.Pivo.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Update attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

    //public bool UpdateAggregate(Pivo model)
    //{
    //    if (_dbContext.Pivo.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
    //    {
    //        var isSuccess = _dbContext.SaveChanges() > 0;

    //        // every Update attaches the entity object and EF begins tracking
    //        // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
    //        _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

    //        return isSuccess;
    //    }

    //    return false;
    //}
}