namespace FruitREST.Model
{

    public record FoodDTO(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity,  string foodTypeName = "", double q10Factor = 3, double maxTemp = 40, double minTemp = -15);



  }  

