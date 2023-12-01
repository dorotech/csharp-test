using CrossCutting.Models;

namespace CrossCutting.Extensions;

public static class ReturnMessageExtensions
{
    public static ReturnMessage<T1> ParseOnlyErros<T2, T1>(this ReturnMessage<T2> returnMessage)
    {
        return new ReturnMessage<T1>(returnMessage.Errors, returnMessage.StatusCode);
    }
}
