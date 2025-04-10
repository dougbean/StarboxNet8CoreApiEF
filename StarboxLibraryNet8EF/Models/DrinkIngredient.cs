namespace StarboxLibraryNet8EF.Models;

public partial class DrinkIngredient
{
    public int DrinkId { get; set; }

    public int IngredientId { get; set; }

    public int IngredientQuantity { get; set; }

    public virtual Drink Drink { get; set; } = null!;

    public virtual Ingredient Ingredient { get; set; } = null!;
}
