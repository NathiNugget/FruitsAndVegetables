namespace FruitREST.Model
{
    public record ReadingDTO(double temperature, double humidity, long? timestamp = null, int? id = null);
}
