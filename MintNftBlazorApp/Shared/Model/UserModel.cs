using System.ComponentModel.DataAnnotations;

namespace MintNftBlazorApp.Shared.Model
{
    public class UserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
