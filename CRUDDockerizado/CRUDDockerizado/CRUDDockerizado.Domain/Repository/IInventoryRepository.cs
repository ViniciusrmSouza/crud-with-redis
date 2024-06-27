using CRUDDockerizado.CRUDDockerizado.Domain.Entities;

namespace CRUDDockerizado.CRUDDockerizado.Domain.Repository;

public interface IInventoryRepository
{
    Task<List<Inventory>> GetAllItem();
    
    Task<Inventory?> GetItem(Guid id);
    Task<Inventory> PostItem(Inventory item);
    Task<Inventory> UpdateItem(Inventory item);
}