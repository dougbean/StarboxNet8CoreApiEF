using Moq;
using StarboxLibraryNet8EF.Repository;
using StarboxLibraryNet8EF.Dtos;
using StarboxLibraryNet8EF.Services;

namespace StarboxUnitTests
{
    public class DrinkServiceTests
    {
        private readonly Mock<IDrinkRepository> _mockDrinkRepository;
        private readonly Mock<IIngredientRepository> _mockIngredientRepository;
        private readonly DrinkService _drinkService;

        public DrinkServiceTests()
        {
            _mockDrinkRepository = new Mock<IDrinkRepository>();
            _mockIngredientRepository = new Mock<IIngredientRepository>();
            _drinkService = new DrinkService(_mockDrinkRepository.Object, _mockIngredientRepository.Object);
        }

        [Fact]
        public async Task GetDrinksAsync_ShouldCalculateTotalPrice()
        {
            // Arrange
            var drinks = new List<DrinkDto>
            {
                new DrinkDto
                {
                    Id = 1,
                    Name = "Drink1",
                    Ingredients = new List<IngredientDto>
                    {
                        new IngredientDto { Name = "espresso", UnitCost = 2.5m, Quantity = 1 },
                        new IngredientDto { Name = "steamed milk", UnitCost = 3.5m, Quantity = 2 }
                    }
                },
                new DrinkDto
                {
                    Id = 2,
                    Name = "Drink2",
                    Ingredients = new List<IngredientDto>
                    {
                        new IngredientDto { Name = "coffee", UnitCost = 1.0m, Quantity = 1 },
                        new IngredientDto { Name = "foamed milk", UnitCost = 4.0m, Quantity = 2 }
                    }
                }
            };

            _mockDrinkRepository.Setup(repo => repo.GetDrinksAsync()).ReturnsAsync(drinks);

            // Act
            var result = await _drinkService.GetDrinksAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal(9.5m, resultList[0].Price); // 2.5 + (3.5 * 2)
            Assert.Equal(9.0m, resultList[1].Price); // 1.0 + (4.0 * 2)
        }

        [Fact]
        public async Task GetDrinkAsync_DrinkAvailable_ReturnsDrink()
        {
            // Arrange
            var drinkId = 1;
            var drink = new DrinkDto
            {
                Id = drinkId,
                Ingredients = new List<IngredientDto>
                {
                    new IngredientDto { Id = 1, Quantity = 2 }
                }
            };
            var ingredients = new List<IngredientDto>
            {
                new IngredientDto { Id = 1, Amount = 5 }
            };

            _mockDrinkRepository.Setup(repo => repo.GetDrinkAsync(drinkId)).ReturnsAsync(drink);
            _mockIngredientRepository.Setup(repo => repo.GetIngredientsAsync()).ReturnsAsync(ingredients);

            // Act
            var result = await _drinkService.GetAvailableDrinkAsync(drinkId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(drinkId, result.Id);
            _mockIngredientRepository.Verify(repo => repo.UpdateIngredientAsync(It.IsAny<IngredientDto>()), Times.Once);
        }

        [Fact]
        public async Task GetDrinkAsync_DrinkNotAvailable_ReturnsEmptyDrink()
        {
            // Arrange
            var drinkId = 1;
            var drink = new DrinkDto
            {
                Id = drinkId,
                Ingredients = new List<IngredientDto>
                {
                    new IngredientDto { Id = 1, Quantity = 10 }
                }
            };
            var ingredients = new List<IngredientDto>
            {
                new IngredientDto { Id = 1, Amount = 5 }
            };

            _mockDrinkRepository.Setup(repo => repo.GetDrinkAsync(drinkId)).ReturnsAsync(drink);
            _mockIngredientRepository.Setup(repo => repo.GetIngredientsAsync()).ReturnsAsync(ingredients);

            // Act
            var result = await _drinkService.GetAvailableDrinkAsync(drinkId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
            _mockIngredientRepository.Verify(repo => repo.UpdateIngredientAsync(It.IsAny<IngredientDto>()), Times.Never);
        }

        [Fact]
        public async Task GetDrinkAsync_UpdatesIngredientsCorrectly()
        {
            // Arrange
            var drinkId = 1;
            var drink = new DrinkDto
            {
                Id = drinkId,
                Ingredients = new List<IngredientDto>
                {
                    new IngredientDto { Id = 1, Quantity = 2 }
                }
            };
            var ingredients = new List<IngredientDto>
            {
                new IngredientDto { Id = 1, Amount = 5 }
            };

            _mockDrinkRepository.Setup(repo => repo.GetDrinkAsync(drinkId)).ReturnsAsync(drink);
            _mockIngredientRepository.Setup(repo => repo.GetIngredientsAsync()).ReturnsAsync(ingredients);

            // Act
            var result = await _drinkService.GetAvailableDrinkAsync(drinkId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(drinkId, result.Id);
            _mockIngredientRepository.Verify(repo => repo.UpdateIngredientAsync(It.Is<IngredientDto>(i => i.Id == 1 && i.Amount == 3)), Times.Once);
        }

        [Fact]
        public async Task GetDrinkAsync_CalculatesPriceCorrectly()
        {
            // Arrange
            var drinkId = 1;
            var drink = new DrinkDto
            {
                Id = drinkId,
                Ingredients = new List<IngredientDto>
                {
                    new IngredientDto { Id = 1, UnitCost = 2.5m, Quantity = 1 },
                    new IngredientDto { Id = 2, UnitCost = 1.5m, Quantity = 2 }
                }
            };
            var ingredients = new List<IngredientDto>
            {
                new IngredientDto { Id = 1, Amount = 5 },
                new IngredientDto { Id = 2, Amount = 5 }
            };

            _mockDrinkRepository.Setup(repo => repo.GetDrinkAsync(drinkId)).ReturnsAsync(drink);
            _mockIngredientRepository.Setup(repo => repo.GetIngredientsAsync()).ReturnsAsync(ingredients);

            // Act
            var result = await _drinkService.GetAvailableDrinkAsync(drinkId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(drinkId, result.Id);
            Assert.Equal(5.5m, result.Price); // 2.5 + (1.5 * 2)
        }
    }
}
