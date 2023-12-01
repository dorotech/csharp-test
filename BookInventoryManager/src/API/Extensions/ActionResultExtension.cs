using System.Text.Json;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;

public static class ActionResultExtensions
{
    public static ActionResult<ReturnMessage<T>> ToActionResult<T>(this ReturnMessage<T> returnMessage, ILogger logger = null)
    {
        if(logger != null)
            if(returnMessage.IsSuccess)
                logger.LogInformation("{Response}", JsonSerializer.Serialize(returnMessage));
            else
                logger.LogError("{Response}",JsonSerializer.Serialize(returnMessage));
        
        return new ObjectResult(returnMessage)
        {
            StatusCode = returnMessage.StatusCode
        };
    }
    
    public static ActionResult<ReturnMessage> ToActionResult(this ReturnMessage returnMessage, ILogger logger = null)
    {
        if(logger != null)
            if(returnMessage.IsSuccess)
                logger.LogInformation("{Response}",JsonSerializer.Serialize(returnMessage));
            else
                logger.LogError("{Response}",JsonSerializer.Serialize(returnMessage));
        
        return new ObjectResult(returnMessage)
        {
            StatusCode = returnMessage.StatusCode
        };
    }
}