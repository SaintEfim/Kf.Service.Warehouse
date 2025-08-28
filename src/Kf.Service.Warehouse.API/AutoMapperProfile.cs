using AutoMapper;
using Kf.Service.Warehouse.API.Models.Inventory;
using Kf.Service.Warehouse.API.Models.Warehouse;
using Kf.Service.Warehouse.Domain.Models;

namespace Kf.Service.Warehouse.API;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        MapWarehouses();
        MapInventories();
    }

    private void MapWarehouses()
    {
        CreateMap<WarehouseModel, WarehouseDto>()
            .ReverseMap();

        CreateMap<WarehouseCreateDto, WarehouseModel>();

        CreateMap<WarehouseUpdateDto, WarehouseModel>()
            .ReverseMap();
    }

    private void MapInventories()
    {
        CreateMap<InventoryModel, InventoryDto>()
            .ReverseMap();
    }
}
