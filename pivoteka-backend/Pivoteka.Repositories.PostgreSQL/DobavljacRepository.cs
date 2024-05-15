using Pivoteka.Commons;
using Pivoteka.DataAccess.PostgreSQL.Data;
using Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Pivoteka.Repositories.PostgreSQL;
public class DobavljacRepository : IDobavljacRepository<string, Dobavljac>
{
    private readonly PivotekaContext _dbContext;

    public DobavljacRepository(PivotekaContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Dobavljac model)
    {
        return _dbContext.Dobavljac
                         .AsNoTracking()
                         .Contains(model);
    }

    public bool Exists(string ime)
    {
        var model = _dbContext.Dobavljac
                              .AsNoTracking()
                              .FirstOrDefault(dobavljac => dobavljac.Ime.Equals(ime));
        return model is not null;
    }

    public Option<Dobavljac> Get(string ime)
    {
        var model = _dbContext.Dobavljac
                              .AsNoTracking()
                              .FirstOrDefault(dobavljac => dobavljac.Ime.Equals(ime));

        return model is not null
            ? Options.Some(model)
            : Options.None<Dobavljac>();
    }

    //public Option<Dobavljac> GetAggregate(string ime)
    //{
    //    var model = _dbContext.Dobavljac
    //                          .Include(dobavljac => dobavljac.DobavljacRoles)
    //                          .ThenInclude(dobavljacRoles => dobavljacRoles.Role)
    //                          .AsNoTracking()
    //                          .FirstOrDefault(dobavljac => dobavljac.Ime.Equals(ime)); // give me the first or null; substitute for .Where()
    //                                                                           // single or default throws an exception if more than one element meets the criteria

    //    return model is not null
    //        ? Options.Some(model)
    //        : Options.None<Dobavljac>();
    //}

    public IEnumerable<Dobavljac> GetAll()
    {
        var models = _dbContext.Dobavljac
                               .ToList();

        return models;
    }

    //public IEnumerable<Dobavljac> GetAllAggregates()
    //{
    //    var models = _dbContext.Dobavljac
    //                           .Include(dobavljac => dobavljac.DobavljacRoles)
    //                           .ThenInclude(dobavljacRoles => dobavljacRoles.Role)
    //                           .ToList();

    //    return models;
    //}

    public bool Insert(Dobavljac model)
    {
        if (_dbContext.Dobavljac.Add(model).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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
        var model = _dbContext.Dobavljac
                              .AsNoTracking()
                              .FirstOrDefault(dobavljac => dobavljac.Ime.Equals(ime));

        if (model is not null)
        {
            _dbContext.Dobavljac.Remove(model);

            return _dbContext.SaveChanges() > 0;
        }
        return false;
    }

    public bool Update(Dobavljac model)
    {
        // detach
        if (_dbContext.Dobavljac.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Update attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

    //public bool UpdateAggregate(Dobavljac model)
    //{
    //    if (_dbContext.Dobavljac.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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