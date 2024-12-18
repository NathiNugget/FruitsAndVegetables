﻿using FruitClassLib;

namespace FruitREST.Model
{
    public static class FoodDTOConverter
    {
        public static Food DTO2Food(FoodDTO dto)
        {
            return new Food(dto.name, dto.foodTypeId, dto.apiLink, dto.spoilDate, dto.spoilHours, dto.idealTemperature, dto.idealHumidity, 1, "", dto.q10Factor, dto.maxTemp, dto.minTemp);
        }
    }
}
