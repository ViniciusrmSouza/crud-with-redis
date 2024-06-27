using System.Text.Json;
using CRUDDockerizado.CRUDDockerizado.Application.Services.Interfaces;
using CRUDDockerizado.CRUDDockerizado.Domain.Caching;
using CRUDDockerizado.CRUDDockerizado.Domain.Entities;
using CRUDDockerizado.CRUDDockerizado.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRUDDockerizado.CRUDDockerizado.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
         var items = await _inventoryService.GetAll();
         if (items == null) return BadRequest("Não existe nenhum item cadastrado");

         return Ok(items);
    }
    
    [HttpGet("Redis")]
    public async Task<IActionResult> Redis(Guid id)
    {
      var result = await _inventoryService.GetById(id.ToString());

      if (result != null) return Ok(result);

      return BadRequest("Esse item não existe!");
    }
    
    [HttpPost("PostItem")]
    public async Task<IActionResult> PostItem(Inventory item)
    {
        var result = await _inventoryService.IncludeItem(item);
        if (result != null) return Ok(result);

        return BadRequest("Item já cadastrado!");
    }

    [HttpPut("PutItem")]
    public async Task<IActionResult> PutItem(Inventory item)
    {
        var result = await _inventoryService.UpdateItem(item);

        if (result != null) return Ok(result);

        return BadRequest("Esse item não existe");
    }
}