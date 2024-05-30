using CRUDDockerizado.CRUDDockerizado.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDDockerizado.CRUDDockerizado.Infrastructure.Data;

public class CrudContext : DbContext
{
    public CrudContext(DbContextOptions<CrudContext> options) : base(options) { }

    public DbSet<Inventory> Inventories { get; set; }
}