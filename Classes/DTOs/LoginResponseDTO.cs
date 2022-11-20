namespace dorotec_backend_test.Classes.DTOs;

/// <summary> Informações de resposta à uma requisição de login. </summary>
public class LoginResponseDTO
{
    public LoginResponseDTO(string name, string token)
    {
        this.Name = name;
        this.Token = token;
    }

    /// <summary> Nome do Administrador. </summary>
    public string Name { get; set; }

    /// <summary> Token de acesso JWT. </summary>
    public string Token { get; set; }
}
