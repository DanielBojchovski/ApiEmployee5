using System.ComponentModel.DataAnnotations;

namespace ApiEmployee5.Models
{
    public class Employee
    {
        [Key]
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "First Name is required./ Името е задолжително")]
        [MinLength(2, ErrorMessage = "FirstName must contain at least 2 characters./ Името мора да содржи барем две букви.")]
        [MaxLength(50, ErrorMessage = "FirstName can't contain more than 50 characters./ Името не смее да содржи повеќе од 50 букви.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name is required./ Презимето е задолжително")]
        [MinLength(2, ErrorMessage = "LastName must contain at least 2 characters./ Презимето мора да содржи барем две букви.")]
        [MaxLength(50, ErrorMessage = "LastName can't contain more than 50 characters./ Презимето не смее да содржи повеќе од 50 букви.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Employee's salary is required./ Платата на вработениот е задолжителна")]
        [Range(18000, 617500, ErrorMessage = "Salary must be in range between 18000 and 617500 denars./ Платата мора да биде во опсег помеѓу 18000 и 617500 денари.")]
        public decimal Salary { get; set; }


        [Required(ErrorMessage = "Employee's email address is required./ Email адресата на вработениот е задолжителна")]
        [StringLength(50, ErrorMessage = "Email's length cannot exceed 50 characters./ Email адресата не смее да надмине 50 знаци")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Employee's date of birth is required./ Датата на раѓање на вработениот е задолжителна")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBrith { get; set; }


        public Gender? Gender { get; set; }


        [Display(Name = "Photo Path")]
        public string? PhotoPath { get; set; }


        [Required(ErrorMessage = "Department id is required./ Идентификациониот број на секторот е задолжителен")]
        [Display(Name = "Department ID")]
        public int DepartmentId { get; set; }


        [Required(ErrorMessage = "Employee's department is required./ Секторот на вработениот е задолжителен")]
        public Department Department { get; set; }


        public string? Skills { get; set; }
    }
}
