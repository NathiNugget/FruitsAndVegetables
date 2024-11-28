
namespace FruitClassLib
{
    public interface IFoodDB
    {
        Food Add(Food food);
        Food FindByName(string name);
        List<Food> GetAll(bool? filterFruit = null, bool? filterVegetable = null);
        void Nuke();
        void Setup();

        List<string> GetAllNames();
    }
}