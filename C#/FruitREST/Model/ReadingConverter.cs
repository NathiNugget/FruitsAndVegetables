using FruitClassLib;

namespace FruitREST.Model
{
    public static class ReadingConverter
    {
        public static Reading DTO2Reading(ReadingDTO dto)
        {
            return new Reading(dto.temperature, dto.humidity);
        }
    }
}
