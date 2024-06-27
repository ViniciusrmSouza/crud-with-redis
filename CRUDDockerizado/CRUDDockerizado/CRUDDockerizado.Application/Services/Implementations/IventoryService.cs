using System.Text.Json;
using CRUDDockerizado.CRUDDockerizado.Application.Services.Interfaces;
using CRUDDockerizado.CRUDDockerizado.Domain.Caching;
using CRUDDockerizado.CRUDDockerizado.Domain.Entities;
using CRUDDockerizado.CRUDDockerizado.Domain.Repository;

namespace CRUDDockerizado.CRUDDockerizado.Application.Services.Implementations;

public class IventoryService : IInventoryService
{
    private readonly ICachingService _cache;
    private IInventoryRepository _inventoryRepository;

    public IventoryService(ICachingService cache, IInventoryRepository inventoryRepository)
    {
        _cache = cache;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<List<Inventory>?> GetAll()
    {
        var items = await _inventoryRepository.GetAllItem();
        return items;
    }

    public async Task<Inventory?> IncludeItem(Inventory item)
    {
       var itemExist = await GetById(item.Id.ToString());
       if (itemExist == null)
       {
           return await _inventoryRepository.PostItem(item);
       }

       return null;
    }

    public async Task<Inventory?> UpdateItem(Inventory item)
    {
        var itemExist = await GetById(item.Id.ToString());
        if (itemExist != null)
        {
            return await _inventoryRepository.UpdateItem(item);
        }

        return null;
    }

    public async Task<Inventory?> GetById(string key)
    {
        var itemCache = await _cache.GetAsync(key);

        Inventory inventoryItem;
        if (itemCache != "Nenhum valor encontrado")
        {
            inventoryItem = JsonSerializer.Deserialize<Inventory>(itemCache)!;
            return inventoryItem;
        }

        inventoryItem = await _inventoryRepository.GetItem(new Guid(key));

        await _cache.SetAsync(key, JsonSerializer.Serialize(inventoryItem));

        return inventoryItem;
    }
}