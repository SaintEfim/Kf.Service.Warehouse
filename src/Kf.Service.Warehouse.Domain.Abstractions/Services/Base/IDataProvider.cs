using Kf.Service.Warehouse.Domain.Models.Base;

namespace Kf.Service.Warehouse.Domain.Services.Base;

public interface IDataProvider<TModel>
    where TModel : class, IModel
{
    Task<IEnumerable<TModel>> Get(
        bool withInclude = false,
        CancellationToken cancellationToken = default);

    Task<TModel> GetOneById(
        Guid id,
        bool withInclude = false,
        CancellationToken cancellationToken = default);
}
