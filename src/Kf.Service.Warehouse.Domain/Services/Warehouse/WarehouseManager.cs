using AutoMapper;
using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Data.Repositories;
using Kf.Service.Warehouse.Domain.Models;
using Kf.Service.Warehouse.Domain.Services.Base;

namespace Kf.Service.Warehouse.Domain.Services.Warehouse;

public class WarehouseManager : DataManagerBase<WarehouseModel, WarehouseEntity, IWarehouseRepository>
{
    public WarehouseManager(
        IMapper mapper,
        IWarehouseRepository repository)
        : base(mapper, repository)
    {
    }
}
