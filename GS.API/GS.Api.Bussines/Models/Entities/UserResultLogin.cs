using System.ComponentModel.DataAnnotations;

namespace GS.Bussines.Models.Entities
{
    public class UserResultLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioToken UserToken { get; set; }
    }
}
