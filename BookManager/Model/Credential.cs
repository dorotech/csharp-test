namespace BookManager.Model
{
    public class Credential
    {
        public Credential()
        {
            email = string.Empty;
            password = string.Empty;
        }
        public string email { get; set; }
        public string password { get; set; }
    }
}
