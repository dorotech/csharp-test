namespace APIBook.Repository
{
    // Simulação de um banco de dados com usuarios simples
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            // demo user e admin
            users.Add(new User { ID = 0, Username = "Admin", Password = "AIAIAIAI", Role = "admin" });
            users.Add(new User { ID = 1, Username = "shadowy", Password = "123", Role = "user" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).First();
        }

    }

}
