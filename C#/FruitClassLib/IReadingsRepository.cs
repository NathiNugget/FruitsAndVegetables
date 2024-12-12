namespace FruitClassLib
{
    public interface IReadingsRepository
    {
        Reading Add(Reading reading);
        List<Reading> Get(int? offset, int? count);
        void Nuke();
        void Setup();
    }
}