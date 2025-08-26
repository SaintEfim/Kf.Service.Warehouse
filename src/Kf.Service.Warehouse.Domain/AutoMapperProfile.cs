using AutoMapper;
using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Domain.Models;

namespace Kf.Service.Warehouse.Domain;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<WarehouseEntity, WarehouseModel>()
            .ForMember(dest => dest.Inventories, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<InventoryEntity, InventoryModel>()
            .ReverseMap();
    }
}
