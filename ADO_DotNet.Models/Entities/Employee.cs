
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADO_DotNet.Models.Entities
{
    public class Employee : BaseModel
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; }

        public Department Department { get; set; }  // navigation property

        [NotMapped]
        public IFormFile Upload { get; set; }
    }
}
