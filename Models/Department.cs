using System.ComponentModel.DataAnnotations;

namespace ApiEmployee5.Models
{
    public class Department
    {
        [Key]
        [Display(Name = "Department ID")]
        public int DepartmentId { get; set; }


        [Required(ErrorMessage = "Department name is required./ Името на секторот е задолжително")]
        [MinLength(2, ErrorMessage = "Department name must contain at least 2 characters. / Името на секторот мора да содржи барем две букви.")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}
