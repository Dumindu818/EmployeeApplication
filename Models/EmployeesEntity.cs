using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApplication.Models
{
    public class EmployeesEntity
    {
        [Key]
        [DisplayName("Employee Id")]
        public int Id { get; set; }

        [DisplayName("Employee Name")]
        public string Name { get; set; }

        [DisplayName("Email")]
        public string Email {  get; set; }

        [DisplayName("Job Position")]
        public string JobPosition { get; set; } 

    }
}
