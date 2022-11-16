namespace BookstoreManager.Domain.dto.ErrorDto
{
    public class GetAllLogErrorResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = String.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
