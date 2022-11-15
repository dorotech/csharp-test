namespace BookstoreManager.Domain.dto.GetAll
{
  public  class GetAllBookResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public bool Active { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
