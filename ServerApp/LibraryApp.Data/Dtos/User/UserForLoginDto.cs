using System.ComponentModel.DataAnnotations;


namespace LibraryApp.Data.Dtos.User
{
    public class UserForLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
