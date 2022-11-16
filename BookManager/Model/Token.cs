using System;

namespace BookManager.Model
{
    public class Token
    {
        public string? token { get; set; }
        public DateTime expires { get; set; }
    }
}