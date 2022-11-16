namespace BookstoreManager.Domain.dto.ErrorDto
{
    public class ViewLogErrorResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = String.Empty;
        public DateTime CreateAt { get; set; }
    };
   
}
