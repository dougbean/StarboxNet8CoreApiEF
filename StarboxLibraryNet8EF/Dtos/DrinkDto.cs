using System.Text.Json.Serialization;

namespace StarboxLibraryNet8EF.Dtos
{
    public class DrinkDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("ingredients")]
        public List<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
    }
}
