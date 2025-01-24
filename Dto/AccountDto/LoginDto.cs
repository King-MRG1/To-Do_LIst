using System.ComponentModel.DataAnnotations;

namespace To_Do_List.Dto.AccountDto
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
