using Moq;
using StarboxLibraryNet8EF.Services;
using StarboxLibraryNet8EF.Repository;
using StarboxLibraryNet8EF.Dtos;

namespace StarboxUnitTests
{
    public class IngredientServiceTests
    {
        private readonly Mock<IIngredientRepository> _mockIngredientRepository;
        private readonly IngredientService _ingredientService;

        public IngredientServiceTests()
        {
            _mockIngredientRepository = new Mock<IIngredientRepository>();
            _ingredientService = new IngredientService(_mockIngredientRepository.Object);
        }

        [Fact]
        public async Task UpdateIngredientsAmountAsync_UpdatesAllIngredients()
        {
            // Arrange
            var ingredients = new List<IngredientDto>
            {
                new IngredientDto { Id = 1, Name = "Ingredient1", Amount = 5 },
                new IngredientDto { Id = 2, Name = "Ingredient2", Amount = 10 }
            };

            _mockIngredientRepository.Setup(repo => repo.GetIngredientsAsync()).ReturnsAsync(ingredients);

            // Act
            await _ingredientService.UpdateIngredientsAmountAsync(20);

            // Assert
            foreach (var ingredient in ingredients)
            {
                Assert.Equal(20, ingredient.Amount);
                _mockIngredientRepository.Verify(repo => repo.UpdateIngredientAsync(It.Is<IngredientDto>(i => i.Id == ingredient.Id && i.Amount == 20)), Times.Once);
            }
        }
    }
}