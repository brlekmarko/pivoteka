namespace Pivoteka.Repositories;

/// <summary>
/// Facade interface for a Narudzba repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface INarudzbaRepository<TKey, TModel> : IRepository<TKey, TModel>, IAggregateRepository<TKey, TModel>
{
}