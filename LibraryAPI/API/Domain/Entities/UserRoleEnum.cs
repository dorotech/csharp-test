using System.ComponentModel;

namespace LibraryApi.Domain.Entities
{
    public enum UserRoleEnum
    {
        [Description("Básico")]
        BASICO = 1,

        [Description("Administrador")]
        ADMINISTRADOR = 2
    }
}