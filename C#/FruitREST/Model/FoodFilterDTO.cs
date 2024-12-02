namespace FruitREST.Model
{
    public record FoodFilterDTO(int? offset, int? count, bool? filterFruit = null,bool? filterVegetable = null, string? filterName = null);
    
    
}
