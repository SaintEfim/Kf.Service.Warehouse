namespace Kf.Service.Warehouse.Data.Repositories;

public interface IRepositoryBase<TEntity>
{
    Task<TEntity> Get(
        CancellationToken cancellationToken = default);

    Task<TEntity> GetById(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<TEntity> Create(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task Delete(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<TEntity> Update(
        TEntity entity,
        CancellationToken cancellationToken = default);
}
