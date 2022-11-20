namespace DoroTechChallenge.Services.Responses;

public class SignInResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpires { get; set; }
}
