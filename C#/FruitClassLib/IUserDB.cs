namespace FruitClassLib
{
    public interface IUserDB
    {
        public User? Get(string username, string password);

        public string? GetNewSessionToken(string username, string password);
        public bool Validate(string sessionToken);

        public void SetUp();

        public void Nuke();

        public User Add(User user);

        public bool ResetSessionToken(string sessionToken);
    }
}