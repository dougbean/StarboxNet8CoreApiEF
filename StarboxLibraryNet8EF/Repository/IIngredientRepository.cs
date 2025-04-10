using StarboxLibraryNet8EF.Dtos;
using StarboxLibraryNet8EF.Models;

namespace StarboxLibraryNet8EF.Repository
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {        
        Task<IEnumerable<IngredientDto>> GetIngredientsAsync();
        Task<IngredientDto> GetIngredientAsync(int id);        
        Task<IngredientDto> AddIngredientAsync(IngredientDto dto);
        Task UpdateIngredientAsync(IngredientDto dto);        
    }
}
