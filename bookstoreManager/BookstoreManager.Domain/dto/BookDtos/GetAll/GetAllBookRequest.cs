namespace BookstoreManager.Domain.dto.GetAll
{
    public  class GetAllBookRequest 
    {
        public string? Search { get; set; }
        public int Page { get; set; } 
        public int PageSize { get; set; } 
    }
   
}
