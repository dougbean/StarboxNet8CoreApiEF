using System.Text.Json.Serialization;

namespace StarboxLibraryNet8EF.Dtos
{
    public class IngredientDto 
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("unitCost")]
        public decimal UnitCost { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }    
    }
}
