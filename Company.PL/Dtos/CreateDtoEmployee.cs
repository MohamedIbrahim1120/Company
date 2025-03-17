using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class CreateDtoEmployee
    {
        [Required(ErrorMessage =" Name Is Required !")]
        public string EmpName { get; set; }

        [Range(1,60, ErrorMessage = "Age Must Be Between 10 And 60")]
        public int? Age { get; set; }

        [DataType(DataType.EmailAddress,ErrorMessage = "Is Required !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Is Required !")]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = " IsActive Is Required !")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "IsDeleted Is Required !")]
        public bool IsDeleted { get; set; }

        [DisplayName("Hiring Date")]
        [Required(ErrorMessage = " HiringDateIs Required !")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Date Of Creation")]

        [Required(ErrorMessage = "CreateAt Is Required !")]
        public DateTime CreateAt { get; set; }

        public int? DepartmentId { get; set; }
    }
}
