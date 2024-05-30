using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDDockerizado.CRUDDockerizado.Domain.Entities;

[Table("inventory")]
public class Inventory
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("item_name")]
    public string ItemName { get; set; }

    [Column("total_item")]
    public int TotalItem { get; set; }
    
    [Column("item_description")]
    public string ItemDescription { get; set; }
}