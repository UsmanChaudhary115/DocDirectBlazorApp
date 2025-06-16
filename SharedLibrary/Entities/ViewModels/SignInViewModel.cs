using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
    public class SignInViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
