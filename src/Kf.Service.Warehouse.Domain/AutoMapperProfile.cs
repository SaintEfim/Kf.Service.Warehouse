using AutoMapper;
using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Domain.Models;

namespace Kf.Service.Warehouse.Domain;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<WarehouseEntity, WarehouseModel>()
            .ReverseMap()
            .ForMember(dest => dest.Inventories, opt => opt.Ignore());

        CreateMap<InventoryEntity, InventoryModel>()
            .ReverseMap();
    }
}
