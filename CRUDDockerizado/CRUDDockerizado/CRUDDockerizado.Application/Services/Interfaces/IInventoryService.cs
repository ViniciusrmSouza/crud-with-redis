using CRUDDockerizado.CRUDDockerizado.Domain.Entities;

namespace CRUDDockerizado.CRUDDockerizado.Application.Services.Interfaces;

public interface IInventoryService
{
    Task<List<Inventory>?> GetAll();
    Task<Inventory?> IncludeItem(Inventory item);
    Task<Inventory?> UpdateItem(Inventory item);
    Task<Inventory?> GetById(string key);
}