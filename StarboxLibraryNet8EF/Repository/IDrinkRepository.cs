using StarboxLibraryNet8EF.Dtos;
using StarboxLibraryNet8EF.Models;

namespace StarboxLibraryNet8EF.Repository
{
    public interface IDrinkRepository : IRepository<Drink>
    {       
        Task<IEnumerable<DrinkDto>> GetDrinksAsync();
        Task<DrinkDto> GetDrinkAsync(int id);
        Task<DrinkDto> AddDrinkAsync(DrinkDto drinkDto);
        Task UpdateDrinkAsync(DrinkDto drinkDto);
        Task DeleteDrinkAsync(int id);       
    }
}
