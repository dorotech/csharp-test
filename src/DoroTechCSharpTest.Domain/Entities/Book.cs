namespace DoroTechCSharpTest.Domain.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public int ReleaseYear { get; set; }

        public int? Rating { get; set; }

    }
}
