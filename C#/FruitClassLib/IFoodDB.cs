
namespace FruitClassLib
{
    public interface IFoodDB
    {
        Food Add(Food food);
        List<Food> FindByIsVegetable();
        Food FindByName(string name);
        List<Food> GetAll();
        void Nuke();
        void Setup();
    }
}