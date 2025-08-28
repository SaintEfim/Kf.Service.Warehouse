using AutoMapper;
using Kf.Service.Warehouse.API.Abstractions.Models.Inventory;
using Kf.Service.Warehouse.API.Controllers.Base;
using Kf.Service.Warehouse.Domain.Models;
using Kf.Service.Warehouse.Domain.Services.Inventory;
using Microsoft.AspNetCore.JsonPatch;
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

    [HttpPost]
    [OpenApiOperation(nameof(InventoryCreate))]
    [SwaggerResponse(Status201Created, typeof(InventoryDto))]
    public Task<IActionResult> InventoryCreate(
        [FromBody] InventoryCreateDto payload,
        CancellationToken cancellationToken = default)
    {
        return Create(payload, nameof(InventoryGetById), cancellationToken);
    }

    [HttpPatch("{id:guid}")]
    [OpenApiOperation(nameof(InventoryUpdate))]
    [SwaggerResponse(Status200OK, typeof(InventoryDto))]
    [SwaggerResponse(Status404NotFound, typeof(string))]
    public async Task<IActionResult> InventoryUpdate(
        Guid id,
        [FromBody] JsonPatchDocument<InventoryUpdateDto> patchDocument,
        CancellationToken cancellationToken = default)
    {
        return await Update(id, patchDocument, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation(nameof(InventoryDelete))]
    [SwaggerResponse(Status204NoContent, typeof(string))]
    [SwaggerResponse(Status404NotFound, typeof(string))]
    public async Task<IActionResult> InventoryDelete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await Delete(id, cancellationToken);
    }
}
