namespace BookstoreManager.Domain.dto.GetAll
{
    public  record GetAllBookRequest(string? Search, int Page =1 ,int PageSize = 10);
   
}
