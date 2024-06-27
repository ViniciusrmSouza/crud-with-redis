using CRUDDockerizado.CRUDDockerizado.Domain.Entities;
using CRUDDockerizado.CRUDDockerizado.Domain.Repository;
using CRUDDockerizado.CRUDDockerizado.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRUDDockerizado.CRUDDockerizado.Infrastructure.Repository;

public class InventoryRepository : IInventoryRepository
{
    private readonly CrudContext _crudContext;
    public InventoryRepository(CrudContext crudContext)
    {
        _crudContext = crudContext;
    }
    public async Task<List<Inventory>> GetAllItem()
    {
       var items =  await _crudContext.Inventories.AsNoTracking().ToListAsync();
       return items;
    }

    public async Task<Inventory?> GetItem(Guid id)
    {
        return await _crudContext.Inventories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Inventory> PostItem(Inventory item)
    {
        var a = await _crudContext.Inventories.AddAsync(item);
        await _crudContext.SaveChangesAsync();
        return a.Entity;
    }

    public async Task<Inventory> UpdateItem(Inventory item)
    {
        var itemUpdated = _crudContext.Inventories.Update(item);
        await _crudContext.SaveChangesAsync();
        return itemUpdated.Entity;
    }
}