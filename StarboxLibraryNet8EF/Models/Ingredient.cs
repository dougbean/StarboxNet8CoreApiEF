using System.ComponentModel.DataAnnotations;

namespace StarboxLibraryNet8EF.Models;

public partial class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal UnitCost { get; set; }

    public int Amount { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<DrinkIngredient> DrinkIngredients { get; set; } = new List<DrinkIngredient>();
}
