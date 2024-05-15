namespace Pivoteka.Repositories;

/// <summary>
/// Facade interface for a Narudzba repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface INarudzbaRepository<TKey, TModel> : IRepository<TKey, TModel>, IAggregateRepository<TKey, TModel>
{
    /// <summary>
    /// Updates the entire aggregate
    /// </summary>
    /// <param name="model">Aggregate object</param>
    /// <returns><c>true</c> on success, <c>false</c> on failure</returns>
    bool InsertAggregate(TModel model);
}