namespace Pivoteka.Repositories;

/// <summary>
/// Facade interface for a Pivo repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface INarucioPivoRepository<TKey, TModel>
{
    /// <summary>
    /// Checks if there is enough inventory for the given models
    /// </summary>
    /// <param name="models"></param>
    /// <returns><c>true</c> on success, <c>false</c> on fail</returns>
    bool CheckAmount(IEnumerable<TModel> models);


    /// <summary>
    /// Reduces the amount of inventory for the given models
    /// </summary>
    /// <param name="models"></param>
    /// <returns><c>true</c> on success, <c>false</c> on fail</returns>
    bool ReduceAmount(IEnumerable<TModel> models);
}