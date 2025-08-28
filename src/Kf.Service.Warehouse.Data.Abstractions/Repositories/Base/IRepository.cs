using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Data.Models.Base;
using Sieve.Models;

namespace Kf.Service.Warehouse.Data.Repositories.Base;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<IEnumerable<TEntity>> Get(
        SieveModel? filter,
        bool withIncludes = false,
        CancellationToken cancellationToken = default);

    Task<TEntity> GetOneById(
        Guid id,
        bool withIncludes = false,
        CancellationToken cancellationToken = default);

    Task<TEntity> Create(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task<TEntity> Delete(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<TEntity> Update(
        TEntity entity,
        CancellationToken cancellationToken = default);
}
