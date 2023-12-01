using Application.Common.Responses;
using Application.Features.Authors.Commands.Update;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Books.Commands.Update;

public class UpdateBookCommand : IRequest<ReturnMessage<BookWithPurchasePriceResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
    
    [FromBody]
    public UpdateBookData Data { get; set; }
}