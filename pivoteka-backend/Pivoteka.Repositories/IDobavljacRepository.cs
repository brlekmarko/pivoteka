namespace Pivoteka.Repositories;

/// <summary>
/// Facade interface for a Vrstum repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface IDobavljacRepository<TKey, TModel> : IRepository<TKey, TModel>
{
}