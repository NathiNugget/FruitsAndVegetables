namespace FruitClassLib
{
    public interface IFoodDB
    {
        Food Add(Food food, string? token = null);
        Food FindByName(string name);
        List<Food> GetAllFiltered(bool? filterFruit = null, bool? filterVegetable = null, string? nameFilter = null, int? offset = null, int? count = null);

        List<Food> GetAll(int? offset = null, int? count = null);
        void Nuke();
        void Setup();

        List<string> GetAllNames(bool? filterFruit = null, bool? filterVegetable = null);
    }
}