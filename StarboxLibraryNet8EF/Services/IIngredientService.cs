using StarboxLibraryNet8EF.Dtos;

namespace StarboxLibraryNet8EF.Services
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetIngredientsAsync();
        Task<IngredientDto> GetIngredientAsync(int id);        
        Task<IngredientDto> AddIngredientAsync(IngredientDto dto);
        Task UpdateIngredientAsync(IngredientDto dto);
        Task DeleteIngredientAsync(int id);
        Task UpdateIngredientsAmountAsync(int amount);
    }
}
