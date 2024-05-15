namespace Pivoteka.Repositories;

/// <summary>
/// Facade interface for a Pivo repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface IKorisnikRepository<TKey, TModel> : IRepository<TKey, TModel>
{
}