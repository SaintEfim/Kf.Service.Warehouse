using Kf.Service.Warehouse.Domain.Models.Base;

namespace Kf.Service.Warehouse.Domain.Services.Base;

public interface IDataManager<TModel>
    where TModel : class, IModel
{
    Task<TModel> Create(
        TModel model,
        CancellationToken cancellationToken = default);

    Task<TModel> Update(
        TModel model,
        CancellationToken cancellationToken = default);

    Task<TModel> Delete(
        Guid id,
        CancellationToken cancellationToken = default);
}
