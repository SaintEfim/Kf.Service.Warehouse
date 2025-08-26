using AutoMapper;
using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Data.Repositories;
using Kf.Service.Warehouse.Domain.Models;
using Kf.Service.Warehouse.Domain.Services.Base;

namespace Kf.Service.Warehouse.Domain.Services.Warehouse;

public class WarehouseProvider : DataProviderBase<WarehouseModel, WarehouseEntity, IWarehouseRepository>
{
    public WarehouseProvider(
        IMapper mapper,
        IWarehouseRepository repository)
        : base(mapper, repository)
    {
    }
}
