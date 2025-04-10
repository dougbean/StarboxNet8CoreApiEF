using System.ComponentModel.DataAnnotations;

namespace StarboxLibraryNet8EF.Models;

public partial class Drink
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<DrinkIngredient> DrinkIngredients { get; set; } = new List<DrinkIngredient>();
}
