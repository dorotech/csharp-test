using Newtonsoft.Json;

namespace DoroTechCSharpTest.Application.ViewModel
{
    [JsonObject(Title = "book")]
    public class BookViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }

        public string Author { get; set; }

        public int ReleaseYear { get; set; }

        public int? Rating { get; set; }
    }
}
