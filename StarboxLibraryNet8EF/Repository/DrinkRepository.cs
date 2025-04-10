using Microsoft.EntityFrameworkCore;
using Serilog;
using StarboxLibraryNet8EF.Data;
using StarboxLibraryNet8EF.Models;
using StarboxLibraryNet8EF.Dtos;

namespace StarboxLibraryNet8EF.Repository
{
    public class DrinkRepository : Repository<Drink>, IDrinkRepository
    {        
        private readonly StarboxDbContext _context;
        private readonly DbSet<Drink> _dbSet;
        
        public DrinkRepository(StarboxDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Drink>();
        }
                
        public async Task<IEnumerable<DrinkDto>> GetDrinksAsync()
        {
            var drinkDtos = new List<DrinkDto>();
            try
            {   
                var drinks = await (from d in _context.Drinks
                                 join dr in _context.DrinkIngredients on d.Id equals dr.DrinkId
                                 join i in _context.Ingredients on dr.IngredientId equals i.Id
                                 select new
                                 {
                                     DrinkName = d.Name,
                                     DrinkId = d.Id,
                                     IngredientId = i.Id,
                                     IngredientName = i.Name,
                                     IngredientUnitCost = i.UnitCost,
                                     IngredientQuantity = dr.IngredientQuantity                                    
                                 }).ToListAsync();
                
                var distinctItems = (from d in drinks
                                     select new
                                     {
                                         DrinkName = d.DrinkName,
                                         DrinkId = d.DrinkId                                        
                                     }).ToList().Distinct();              

                foreach (var d in distinctItems)
                {
                    var drinkDto = new DrinkDto() { Id = d.DrinkId, Name = d.DrinkName };

                    var query = from x in drinks
                                where x.DrinkId == d.DrinkId
                                select x; //returns more than one record

                    foreach (var drink in query)
                    {
                        drinkDto.Ingredients.Add(new IngredientDto()
                        {
                            Id = drink.IngredientId,
                            Name = drink.IngredientName,
                            Quantity = drink.IngredientQuantity,
                            UnitCost = drink.IngredientUnitCost,
                        });                       
                    }
                    drinkDtos.Add(drinkDto);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
           return drinkDtos;
        }
       
        public async Task<DrinkDto> GetDrinkAsync(int id)
        {
            var drinkDtos = new List<DrinkDto>();
            try
            {
                //remember, the join brings back multiple records because of multiple ingredients.
                var drinks = await (from d in _context.Drinks
                                    join dr in _context.DrinkIngredients on d.Id equals dr.DrinkId
                                    join i in _context.Ingredients on dr.IngredientId equals i.Id
                                    where d.Id == id
                                    select new
                                    {
                                        DrinkName = d.Name,
                                        DrinkId = d.Id,
                                        IngredientId = i.Id,
                                        IngredientName = i.Name,
                                        IngredientUnitCost = i.UnitCost,
                                        IngredientQuantity = dr.IngredientQuantity
                                    }).ToListAsync();

                var distinctItems = (from d in drinks
                                     select new
                                     {
                                         DrinkName = d.DrinkName,
                                         DrinkId = d.DrinkId
                                     }).ToList().Distinct();

                foreach (var d in distinctItems)
                {
                    var drinkDto = new DrinkDto() { Id = d.DrinkId, Name = d.DrinkName };

                    var query = from x in drinks
                                where x.DrinkId == d.DrinkId
                                select x; //returns more than one record

                    foreach (var drink in query)
                    {                        
                        drinkDto.Ingredients.Add(new IngredientDto()
                        {
                            Id = drink.IngredientId,
                            Name = drink.IngredientName,
                            Quantity = drink.IngredientQuantity,
                            UnitCost = drink.IngredientUnitCost,
                        });
                    }

                    drinkDtos.Add(drinkDto);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return drinkDtos.FirstOrDefault();
        }      

        public async Task<DrinkDto> AddDrinkAsync(DrinkDto drinkDto) 
        {
            DrinkDto createdDto = new DrinkDto();
            using (var transaction =  await _context.Database.BeginTransactionAsync()) 
            {
                try
                {
                    var drink = new Drink() { Name = drinkDto.Name };                   
                    var entityEntry = await _dbSet.AddAsync(drink);                    
                    await _context.SaveChangesAsync();
                   
                    Drink inserted = null;
                    if (entityEntry != null)
                    {
                        inserted = entityEntry.Entity;                       
                        if (inserted != null)
                        {
                            AddIngredients(drinkDto, inserted);
                            await _context.SaveChangesAsync();
                        }
                    }
                   
                    //Get a dto with the drink and ingredient ids
                    if (inserted != null)
                    {                        
                        createdDto = await GetDrinkAsync(inserted.Id);
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {                   
                    await transaction.RollbackAsync();
                    Log.Error(ex.Message);
                }
            }
            return createdDto;
        }

        private void AddIngredients(DrinkDto drinkDto, Drink inserted)
        {
            foreach (var ingredientDto in drinkDto.Ingredients)
            {
                int ingredientId = GetIngredientId(ingredientDto);

                inserted.DrinkIngredients.Add(new DrinkIngredient()
                {
                    DrinkId = inserted.Id,
                    IngredientId = ingredientId,
                    IngredientQuantity = ingredientDto.Quantity
                });
            }
        }

        private int GetIngredientId(IngredientDto ingredientDto)
        {
            int ingredientId = ingredientDto.Id;
            if (ingredientDto.Id == 0)
            {
                var ingredient = (from i in _context.Ingredients
                                  where i.Name == ingredientDto.Name
                                  select i).FirstOrDefault();

                if (ingredient != null)
                {
                    ingredientId = ingredient.Id;
                }
            }

            return ingredientId;
        }

        public async Task UpdateDrinkAsync(DrinkDto drinkDto) 
        {
            using (var transaction = await _context.Database.BeginTransactionAsync()) 
            {
                try
                {
                    var drink = await _context.Drinks.Include(d => d.DrinkIngredients).FirstOrDefaultAsync(d => d.Id == drinkDto.Id);

                    if (drink != null)
                    {
                        //refresh the ingredient list
                        var drinkIngredients = _context.DrinkIngredients.Where(x => x.DrinkId == drink.Id);
                        foreach (var di in drinkIngredients)
                        {
                            drink.DrinkIngredients.Remove(di);
                        }

                        foreach (var di in drinkDto.Ingredients)
                        {
                            int ingredientId = GetIngredientId(di);
                            drink.DrinkIngredients.Add(new DrinkIngredient() { DrinkId = drinkDto.Id, IngredientId = ingredientId, IngredientQuantity = di.Quantity });
                        }

                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }                    
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error(ex.Message);
                }
            }
        }

        public async Task DeleteDrinkAsync(int id) 
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var drink = await _context.Drinks.Include(d => d.DrinkIngredients).FirstOrDefaultAsync(d => d.Id == id);

                    if (drink != null)
                    {
                        var drinkIngredients = _context.DrinkIngredients.Where(x => x.DrinkId == drink.Id);
                        foreach (var di in drinkIngredients)
                        {
                            drink.DrinkIngredients.Remove(di);
                        }
                        _context.Drinks.Remove(drink);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }                   
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error(ex.Message);
                }
            }
        }      
    }
}
