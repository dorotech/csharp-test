namespace dorotec_backend_test.Classes.DTOs;

public class LoginResponseDTO
{
    public LoginResponseDTO(string name, string token)
    {
        this.Name = name;
        this.Token = token;
    }
    public string Name { get; set; }
    public string Token { get; set; }
}
