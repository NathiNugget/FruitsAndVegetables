namespace FruitREST.Model
{

    public record FoodDTO(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity,  string foodTypeName = "");



  }  

