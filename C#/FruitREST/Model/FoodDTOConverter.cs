using FruitClassLib;

namespace FruitREST.Model
{
    public static class FoodDTOConverter
    {
        public static Food DTO2Food(FoodDTO dto)
        {
            return new Food(dto.name, dto.isVegetable, dto.apiLink, dto.spoilDate, dto.spoilHours, dto.idealTemperature, dto.idealHumidity);
        }
    }
}
