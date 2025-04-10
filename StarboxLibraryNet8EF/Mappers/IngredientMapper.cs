using StarboxLibraryNet8EF.Models;
using StarboxLibraryNet8EF.Dtos;

namespace StarboxLibraryNet8EF.Mappers
{
    public class IngredientMapper
    {
        public static IngredientDto ToIngredientDto(Ingredient ingredient)
        {
            return new IngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                UnitCost = ingredient.UnitCost,
                Amount = ingredient.Amount
            };
        }      
    }
}
