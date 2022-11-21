using System.ComponentModel.DataAnnotations;

namespace BookManager.Model
{
    public class BookFilter
    {
        public BookFilter()
        {
            name = string.Empty;
            decription = string.Empty;
            author = string.Empty;
        }
        public string? name { get; set; }
        public string? decription { get; set; }
        public string? author { get; set; }


    }
}
