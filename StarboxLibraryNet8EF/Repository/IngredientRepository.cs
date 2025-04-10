using Microsoft.EntityFrameworkCore;
using Serilog;
using StarboxLibraryNet8EF.Dtos;
using StarboxLibraryNet8EF.Models;
using StarboxLibraryNet8EF.Data;
using StarboxLibraryNet8EF.Mappers;

namespace StarboxLibraryNet8EF.Repository
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        private readonly StarboxDbContext _context;
        private readonly DbSet<Ingredient> _dbSet;

        public IngredientRepository(StarboxDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Ingredient>();
        }

        public async Task<IEnumerable<IngredientDto>> GetIngredientsAsync()
        {
            var ingredientDtos = new List<IngredientDto>();
            try
            {
                var ingredients = await this.GetAllAsync(); 

                if (ingredients != null)
                {
                    //this iterates the collection a second time, which would not be effecient on a big dataset
                    foreach (var ingredient in ingredients) 
                    {
                        var dto = new IngredientDto()
                        {
                            Id = ingredient.Id,
                            Name = ingredient.Name,
                            UnitCost = ingredient.UnitCost,
                            Amount = ingredient.Amount
                        };
                        ingredientDtos.Add(dto);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return ingredientDtos;
        }

        public async Task<IngredientDto> GetIngredientAsync(int id)
        {
            var dto = new IngredientDto();
            try
            {
                var ingredient = await this.GetByIdAsync(id);                

                if (ingredient != null)
                {                    
                    dto = new IngredientDto()
                    {
                        Id = ingredient.Id,
                        Name = ingredient.Name,
                        UnitCost = ingredient.UnitCost,
                        Amount = ingredient.Amount
                    }; 
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return dto;
        }        

        public async Task<IngredientDto> AddIngredientAsync(IngredientDto dto)
        {
            IngredientDto createdDto = new IngredientDto();
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var ingredient = new Ingredient() { Name = dto.Name, Amount = dto.Amount, UnitCost = dto.UnitCost };
                    var entityEntry = await _dbSet.AddAsync(ingredient);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    Ingredient inserted = entityEntry.Entity;
                    if (inserted != null)
                    {
                        createdDto = IngredientMapper.ToIngredientDto(inserted);
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error(ex.Message);
                }
            }
            return createdDto;
        }

        public async Task UpdateIngredientAsync(IngredientDto dto) 
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var ingredient = await this.GetByIdAsync(dto.Id);
                    ingredient.Name = dto.Name;
                    ingredient.UnitCost = dto.UnitCost;
                    ingredient.Amount = dto.Amount;
                                      
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
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
