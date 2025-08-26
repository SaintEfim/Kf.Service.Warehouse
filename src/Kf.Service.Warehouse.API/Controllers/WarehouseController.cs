using AutoMapper;
using Kf.Service.Warehouse.API.Abstractions.Models.Warehouse;
using Kf.Service.Warehouse.API.Controllers.Base;
using Kf.Service.Warehouse.Domain.Models;
using Kf.Service.Warehouse.Domain.Services.Warehouse;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kf.Service.Warehouse.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/v1/warehouse")]
public class WarehouseController
    : ControllerCrudBase<WarehouseDto, WarehouseModel, IWarehouseManager, IWarehouseProvider>
{
    public WarehouseController(
        IMapper mapper,
        IWarehouseManager manager,
        IWarehouseProvider provider)
        : base(mapper, manager, provider)
    {
    }

    [HttpGet]
    [OpenApiOperation(nameof(WarehouseGet))]
    [SwaggerResponse(Status200OK, typeof(List<WarehouseDto>))]
    public async Task<ActionResult<List<WarehouseDto>>> WarehouseGet(
        bool withIncludes = false,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Get(withIncludes, cancellationToken));
    }

    [HttpGet("{id:guid}", Name = nameof(WarehouseGetById))]
    [OpenApiOperation(nameof(WarehouseGetById))]
    [SwaggerResponse(Status200OK, typeof(WarehouseDto))]
    [SwaggerResponse(Status404NotFound, typeof(string))]
    public async Task<ActionResult<WarehouseDto>> WarehouseGetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return Ok(await GetOneById(id, true, cancellationToken));
    }

    [HttpPost]
    [OpenApiOperation(nameof(WarehouseCreate))]
    [SwaggerResponse(Status201Created, typeof(WarehouseDto))]
    public Task<IActionResult> WarehouseCreate(
        [FromBody] WarehouseCreateDto payload,
        CancellationToken cancellationToken = default)
    {
        return Create(payload, nameof(WarehouseGetById), cancellationToken);
    }

    [HttpPatch("{id:guid}")]
    [OpenApiOperation(nameof(WarehouseUpdate))]
    [SwaggerResponse(Status200OK, typeof(WarehouseDto))]
    [SwaggerResponse(Status404NotFound, typeof(string))]
    public async Task<IActionResult> WarehouseUpdate(
        Guid id,
        [FromBody] JsonPatchDocument<WarehouseUpdateDto> patchDocument,
        CancellationToken cancellationToken = default)
    {
        return await Update(id, patchDocument, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation(nameof(WarehouseDelete))]
    [SwaggerResponse(Status204NoContent, typeof(void))]
    [SwaggerResponse(Status404NotFound, typeof(string))]
    public async Task<IActionResult> WarehouseDelete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await Delete(id, cancellationToken);
    }
}
