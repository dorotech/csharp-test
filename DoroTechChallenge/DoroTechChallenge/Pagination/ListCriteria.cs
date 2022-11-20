using System.Collections.Generic;

namespace Kumbajah.Services.Pagination;

public class ListCriteria
{
    public Filter Filter { get; set; }
    public PagedResponse Pagination { get; set; }
}
