using StarboxLibraryNet8EF.Dtos;

namespace StarboxLibraryNet8EF.Services
{
    public interface IDrinkService
    {
        Task<IEnumerable<DrinkDto>> GetDrinksAsync();
        Task<DrinkDto> GetAvailableDrinkAsync(int id);
        Task<DrinkDto> GetDrinkAsync(int id);
        Task<DrinkDto> AddDrinkAsync(DrinkDto drinkDto);
        Task UpdateDrinkAsync(DrinkDto drinkDto);
        Task DeleteDrinkAsync(int id);        
    }
}
