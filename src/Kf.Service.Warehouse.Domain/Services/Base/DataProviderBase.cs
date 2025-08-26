using AutoMapper;
using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Data.Repositories.Base;
using Kf.Service.Warehouse.Domain.Models.Base;

namespace Kf.Service.Warehouse.Domain.Services.Base;

public abstract class DataProviderBase<TModel, TEntity, TRepository> : IDataProvider<TModel>
    where TModel : class, IModel
    where TEntity : class, IEntity
    where TRepository : IRepository<TEntity>
{
    protected DataProviderBase(
        IMapper mapper,
        TRepository repository)
    {
        Mapper = mapper;
        Repository = repository;
    }

    protected IMapper Mapper { get; }
    protected TRepository Repository { get; }

    public async Task<TModel> Get(
        bool withInclude = false,
        CancellationToken cancellationToken = default)
    {
        return Mapper.Map<TModel>(await Repository.Get(withInclude, cancellationToken));
    }

    public async Task<TModel> GetOneById(
        Guid id,
        bool withInclude = false,
        CancellationToken cancellationToken = default)
    {
        return Mapper.Map<TModel>(await Repository.GetOneById(id, withInclude, cancellationToken));
    }
}
