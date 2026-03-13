namespace FlightBookingSystem.Repository;

/// <summary>
/// Defines basic CRUD operations for a repository.
/// </summary>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
/// <typeparam name="TEntity">The type of the domain entity.</typeparam>
public interface IRepository<TId, TEntity>
{
    /// <summary>
    /// Saves a new entity to the data store.
    /// </summary>
    /// <param name="entity">The entity to save.</param>
    void Save(TEntity entity);

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>The found entity or null if not found.</returns>
    TEntity FindOne(TId id);

    /// <summary>
    /// Retrieves all entities of the specified type.
    /// </summary>
    /// <returns>An enumerable collection of all entities.</returns>
    IEnumerable<TEntity> FindAll();

    /// <summary>
    /// Updates an existing entity in the data store.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Deletes an entity from the data store by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    void Delete(TId id);
}