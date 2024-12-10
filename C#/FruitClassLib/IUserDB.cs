namespace FruitClassLib
{
    public interface IUserDB
    {
        User? Get(string username, string password);
        bool Validate();
    }
}