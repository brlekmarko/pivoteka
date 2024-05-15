using Pivoteka.Commons;
using Pivoteka.DataAccess.PostgreSQL.Data;
using Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Pivoteka.Repositories.PostgreSQL;
public class VrstumRepository : IVrstumRepository<string, Vrstum>
{
    private readonly PivotekaContext _dbContext;

    public VrstumRepository(PivotekaContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Vrstum model)
    {
        return _dbContext.Vrstum
                         .AsNoTracking()
                         .Contains(model);
    }

    public bool Exists(string ime)
    {
        var model = _dbContext.Vrstum
                              .AsNoTracking()
                              .FirstOrDefault(vrsta => vrsta.Ime.Equals(ime));
        return model is not null;
    }

    public Option<Vrstum> Get(string ime)
    {
        var model = _dbContext.Vrstum
                              .AsNoTracking()
                              .FirstOrDefault(vrsta => vrsta.Ime.Equals(ime));

        return model is not null
            ? Options.Some(model)
            : Options.None<Vrstum>();
    }

    //public Option<Vrstum> GetAggregate(string ime)
    //{
    //    var model = _dbContext.Vrstum
    //                          .Include(vrsta => vrsta.VrstumRoles)
    //                          .ThenInclude(vrstaRoles => vrstaRoles.Role)
    //                          .AsNoTracking()
    //                          .FirstOrDefault(vrsta => vrsta.Ime.Equals(ime)); // give me the first or null; substitute for .Where()
    //                                                                           // single or default throws an exception if more than one element meets the criteria

    //    return model is not null
    //        ? Options.Some(model)
    //        : Options.None<Vrstum>();
    //}

    public IEnumerable<Vrstum> GetAll()
    {
        var models = _dbContext.Vrstum
                               .ToList();

        return models;
    }

    //public IEnumerable<Vrstum> GetAllAggregates()
    //{
    //    var models = _dbContext.Vrstum
    //                           .Include(vrsta => vrsta.VrstumRoles)
    //                           .ThenInclude(vrstaRoles => vrstaRoles.Role)
    //                           .ToList();

    //    return models;
    //}

    public bool Insert(Vrstum model)
    {
        if (_dbContext.Vrstum.Add(model).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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
        var model = _dbContext.Vrstum
                              .AsNoTracking()
                              .FirstOrDefault(vrsta => vrsta.Ime.Equals(ime));

        if (model is not null)
        {
            _dbContext.Vrstum.Remove(model);

            return _dbContext.SaveChanges() > 0;
        }
        return false;
    }

    public bool Update(Vrstum model)
    {
        // detach
        if (_dbContext.Vrstum.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
        {
            var isSuccess = _dbContext.SaveChanges() > 0;

            // every Update attaches the entity object and EF begins tracking
            // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
            _dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            return isSuccess;
        }

        return false;
    }

    //public bool UpdateAggregate(Vrstum model)
    //{
    //    if (_dbContext.Vrstum.Update(model).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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