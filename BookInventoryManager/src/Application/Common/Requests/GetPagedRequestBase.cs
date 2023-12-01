using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Application.Common.Requests;

public class GetPagedRequestBase
{
    [Required]
    [FromQuery(Name = "page-index")]
    public int PageIndex { get; set; }

    [Required]
    [FromQuery(Name = "page-size")]
    public int PageSize { get; set; }
}