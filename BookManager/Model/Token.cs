namespace BookManager.Model
{
    public class Token
    {
        public int id { get; set; }
        public string? token { get; set; }
        public DateTime expires { get; set; }
    }
}