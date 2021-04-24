using System.ComponentModel.DataAnnotations;

namespace GS.Bussines.Models.Entities
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Campo Obrigatótio!")]
        [EmailAddress(ErrorMessage = "Preencha o campo corretamente")]
        [StringLength(120, ErrorMessage = "Máximo de catacteres é 120")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatótio!")]
        [StringLength(20, ErrorMessage = "Máximo de catacteres é 20", MinimumLength = 6)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Campos não estão iguais")]
        public string ConfirmarSenha { get; set; }
    }
}
