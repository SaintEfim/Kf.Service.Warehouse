using AutoMapper;
using Kf.Service.Warehouse.API.Controllers.Base;
using Kf.Service.Warehouse.API.Models.Inventory;
using Kf.Service.Warehouse.Domain.Models;
using Kf.Service.Warehouse.Domain.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Sieve.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kf.Service.Warehouse.API.Controllers;

[Route("api/v1/inventories")]
public class InventoryController
    : ControllerCrudBase<InventoryDto, InventoryModel, IInventoryManager, IInventoryProvider>
{
    public InventoryController(
        IMapper mapper,
        IInventoryManager manager,
        IInventoryProvider provider)
        : base(mapper, manager, provider)
    {
    }

    [HttpGet]
    [OpenApiOperation(nameof(InventoryGet))]
    [SwaggerResponse(Status200OK, typeof(List<InventoryDto>))]
    public async Task<ActionResult<List<InventoryDto>>> InventoryGet(
        [FromQuery] SieveModel filter,
        bool withIncludes = false,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Get(filter, withIncludes, cancellationToken));
    }

    [HttpGet("{id:guid}", Name = nameof(InventoryGetById))]
    [OpenApiOperation(nameof(InventoryGetById))]
    [SwaggerResponse(Status200OK, typeof(InventoryDto))]
    [SwaggerResponse(Status404NotFound, typeof(string))]
    public async Task<ActionResult<InventoryDto>> InventoryGetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return Ok(await GetOneById(id, true, cancellationToken));
    }
}
