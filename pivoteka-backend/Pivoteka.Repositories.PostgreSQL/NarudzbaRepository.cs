using Pivoteka.Commons;
using Pivoteka.DataAccess.PostgreSQL.Data;
using Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Pivoteka.Repositories.PostgreSQL;
public class NarudzbaRepository : INarudzbaRepository<int, Narudzba>
{
    private readonly PivotekaContext _dbContext;

    public NarudzbaRepository(PivotekaContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Narudzba model)
    {
        return _dbContext.Narudzba
                         .AsNoTracking()
                         .Contains(model);
    }

    public bool Exists(int id)
    {
        var model = _dbContext.Narudzba
                              .AsNoTracking()
                              .FirstOrDefault(narudzba => narudzba.Id.Equals(id));
        return model is not null;
    }

    public Option<Narudzba> Get(int id)
    {
        var model = _dbContext.Narudzba
                              .AsNoTracking()
                              .FirstOrDefault(narudzba => narudzba.Id.Equals(id));

        return model is not null
            ? Options.Some(model)
            : Options.None<Narudzba>();
    }

    public Option<Narudzba> GetAggregate(int id)
    {
        var model = _dbContext.Narudzba
                              .Include(narudzba => narudzba.NarucioPivos)
                              .AsNoTracking()
                              .FirstOrDefault(narudzba => narudzba.Id.Equals(id)); // give me the first or null; substitute for .Where()
                                                                            // single or default throws an exception if more than one element meets the criteria

        return model is not null
            ? Options.Some(model)
            : Options.None<Narudzba>();
    }

    public IEnumerable<Narudzba> GetAll()
    {
        var models = _dbContext.Narudzba
                               .ToList();

        return models;
    }

    public IEnumerable<Narudzba> GetAllAggregates()
    {
        var models = _dbContext.Narudzba
                               .Include(narudzba => narudzba.NarucioPivos)
                               .ToList();

        return models;
    }

    public bool Insert(Narudzba model)
    {
        if (_dbContext.Narudzba.Add(model).State == Microsoft.EntityFrameworkCore.EntityState.Added)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Add attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

    public bool InsertAggregate(Narudzba model)
    {
        if (_dbContext.Narudzba.Add(model).State == Microsoft.EntityFrameworkCore.EntityState.Added)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;
            // every Add attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return isSuccess;
        }
        return false;
    }

    public bool Remove(int id)
    {
        var model = _dbContext.Narudzba
                              .AsNoTracking()
                              .FirstOrDefault(narudzba => narudzba.Id.Equals(id));

        if (model is not null)
        {
            _dbContext.Narudzba.Remove(model);

            return _dbContext.SaveChanges() > 0;
        }
        return false;
    }

    public bool Update(Narudzba model)
    {
        // detach
        if (_dbContext.Narudzba.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Update attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

    public bool UpdateAggregate(Narudzba model)
    {
        if (_dbContext.Narudzba.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Update attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }
}