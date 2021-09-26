
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADO_DotNet.Models.Entities
{
    public class Department : BaseModel
    {
        [Key]
        public int DeptId { get; set; }

        [Required]
        public string Name { get; set; }        
    }
}
