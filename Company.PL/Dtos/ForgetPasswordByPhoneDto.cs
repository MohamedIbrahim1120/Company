using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class ForgetPasswordByPhoneDto
    {
        [Required(ErrorMessage = "Email is REquries")]
       

        public string PhoneNumer { get; set; }
    }
}
