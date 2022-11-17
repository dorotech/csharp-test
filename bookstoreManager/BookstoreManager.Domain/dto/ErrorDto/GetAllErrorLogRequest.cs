namespace BookstoreManager.Domain.dto.ErrorDto
{
    public class GetAllErrorLogRequest
    {
        public string?  Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
