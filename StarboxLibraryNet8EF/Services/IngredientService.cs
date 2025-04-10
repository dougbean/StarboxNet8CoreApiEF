using StarboxLibraryNet8EF.Repository;
using StarboxLibraryNet8EF.Dtos;

namespace StarboxLibraryNet8EF.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<IEnumerable<IngredientDto>> GetIngredientsAsync()
        {
            var ingredients = await _ingredientRepository.GetIngredientsAsync().ConfigureAwait(false);
            return ingredients;
        }

        public async Task<IngredientDto> GetIngredientAsync(int id)
        {
            var ingredient = await _ingredientRepository.GetIngredientAsync(id).ConfigureAwait(false);
            return ingredient;
        }

        public async Task<IngredientDto> AddIngredientAsync(IngredientDto dto)
        {
            return await _ingredientRepository.AddIngredientAsync(dto).ConfigureAwait(false);
        }

        public async Task UpdateIngredientAsync(IngredientDto dto)
        {
            await _ingredientRepository.UpdateIngredientAsync(dto).ConfigureAwait(false);
        }

        public async Task DeleteIngredientAsync(int id)
        {   
            await _ingredientRepository.DeleteAsync(id).ConfigureAwait(false);          
        }

        /// <summary>
        /// reset the amount of all ingredients.
        /// </summary>
        public async Task UpdateIngredientsAmountAsync(int amount)
        {
            var ingredients = await _ingredientRepository.GetIngredientsAsync().ConfigureAwait(false);
            foreach (var ingredient in ingredients)
            {
                ingredient.Amount = amount;
                await _ingredientRepository.UpdateIngredientAsync(ingredient).ConfigureAwait(false);
            }            
        }        
    }
}
