using StarboxLibraryNet8EF.Repository;
using StarboxLibraryNet8EF.Dtos;

namespace StarboxLibraryNet8EF.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public DrinkService(IDrinkRepository drinkRepository, IIngredientRepository ingredientRepository)
        {
            _drinkRepository = drinkRepository;
            _ingredientRepository = ingredientRepository;
        }
                
        public async Task<IEnumerable<DrinkDto>> GetDrinksAsync()
        {
            var drinks = await _drinkRepository.GetDrinksAsync().ConfigureAwait(false);           
            CalculateDrinkPrice(drinks);
            return drinks;
        }

        private static void CalculateDrinkPrice(IEnumerable<DrinkDto> drinks)
        {
            foreach (var drinkDto in drinks)
            {              
                decimal price = GetPrice(drinkDto);
                drinkDto.Price = price;
            }
        }

        private static decimal GetPrice(DrinkDto drinkDto)
        {
            decimal price = 0;
            foreach (var ingredient in drinkDto.Ingredients)
            {
                decimal ingredientPrice = ingredient.UnitCost * ingredient.Quantity;                
                price += ingredientPrice;
            }

            return price;
        }

        public async Task<DrinkDto> GetAvailableDrinkAsync(int id)
        {
            var drink = await _drinkRepository.GetDrinkAsync(id).ConfigureAwait(false);    
            if (drink == null)
            {
                return new DrinkDto();               
            }

            var ingredients = await _ingredientRepository.GetIngredientsAsync().ConfigureAwait(false);
            if (ingredients == null)
            {
                return new DrinkDto();
            }

            bool isAvailable = IsDrinkAvailable(drink, ingredients);
           
            if (!isAvailable)
            {
                return new DrinkDto();
            }
            else
            {                
                await UpdateIngredientAmountsAsync(drink, ingredients).ConfigureAwait(false);
            }
           
            decimal price = GetPrice(drink);
            drink.Price = price;

            return drink;
        }
                
        public async Task<DrinkDto> GetDrinkAsync(int id)
        {
            var drink = await _drinkRepository.GetDrinkAsync(id).ConfigureAwait(false);
            if (drink == null)
            {
                return new DrinkDto();
            }
            return drink;
        }                
       
        private static bool IsDrinkAvailable(DrinkDto drink, IEnumerable<IngredientDto> ingredients)
        {
            bool isAvailable = true;
            foreach (var ingredient in drink.Ingredients)
            {   
                var ingredientInStore = (from i in ingredients
                                         where i.Id == ingredient.Id
                                         select i).FirstOrDefault();

                if (ingredientInStore != null)
                {
                    if (ingredientInStore.Amount < ingredient.Quantity)
                    {
                        isAvailable = false;
                        break;
                    }
                }
            }

            return isAvailable;
        }

        private async Task UpdateIngredientAmountsAsync(DrinkDto drink, IEnumerable<IngredientDto> ingredients)
        {
            foreach (var ingredientDto in drink.Ingredients)
            {               
                var ingredientInStore = (from i in ingredients
                                         where i.Id == ingredientDto.Id
                                         select i).FirstOrDefault();


                if (ingredientInStore != null)
                {
                    int updatedAmount = ingredientInStore.Amount;
                    updatedAmount -= ingredientDto.Quantity;
                    ingredientInStore.Amount = updatedAmount;
                    await _ingredientRepository.UpdateIngredientAsync(ingredientInStore).ConfigureAwait(false);
                }
            }
        }

        public async Task<DrinkDto> AddDrinkAsync(DrinkDto drinkDto)
        {
           return await _drinkRepository.AddDrinkAsync(drinkDto).ConfigureAwait(false);
        }        

        public async Task UpdateDrinkAsync(DrinkDto drinkDto)
        {           
            await _drinkRepository.UpdateDrinkAsync(drinkDto).ConfigureAwait(false);
        }

        public async Task DeleteDrinkAsync(int id)
        {
            await _drinkRepository.DeleteDrinkAsync(id).ConfigureAwait(false);
        }
    }
}
