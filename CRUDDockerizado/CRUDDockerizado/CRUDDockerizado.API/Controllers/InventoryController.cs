using System.Text.Json;
using CRUDDockerizado.CRUDDockerizado.Domain.Caching;
using CRUDDockerizado.CRUDDockerizado.Domain.Entities;
using CRUDDockerizado.CRUDDockerizado.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRUDDockerizado.CRUDDockerizado.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class InventoryController : ControllerBase
{
    private IInventoryRepository _inventoryRepository;
    private readonly ICachingService _cache;

    public InventoryController(IInventoryRepository inventoryRepository, ICachingService cache)
    {
        _inventoryRepository = inventoryRepository;
        _cache = cache;
    }

    [HttpGet("api/v1/Inventory/GetAll")]
    public async Task<List<Inventory>> GetAll()
    {
       return await _inventoryRepository.GetAllItem();
    }
    
    [HttpGet("api/v1/Inventory/Redis")]
    public async Task<IActionResult> Redis(Guid id)
    {
        var itemCache = await _cache.GetAsync(id.ToString());

        Inventory inventoryItem;
        if (itemCache != "Nenhum valor encontrado")
        {
            inventoryItem = JsonSerializer.Deserialize<Inventory>(itemCache)!;
            return Ok(inventoryItem);
        }

        inventoryItem = await _inventoryRepository.GetItem(id);
        if (inventoryItem == null) NotFound("Item not found");

        await _cache.SetAsync(id.ToString(), JsonSerializer.Serialize(inventoryItem));

        return Ok(inventoryItem);
    }
    
    [HttpPost("api/v1/Inventory/PostItem")]
    public async Task<Inventory> PostItem(Inventory item)
    {
        return await _inventoryRepository.PostItem(item);
    }
    
    [HttpGet("api/v1/Inventory/Teste")]
    public string Teste()
    {
        return "Teste";
    }
}