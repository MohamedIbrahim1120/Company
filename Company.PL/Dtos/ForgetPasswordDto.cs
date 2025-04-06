using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage ="Email is REquries")]
        [EmailAddress]

        public string Email { get; set; }
    }
}
