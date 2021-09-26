using System.ComponentModel.DataAnnotations.Schema;

namespace ADO_DotNet.Models.Entities
{
    public abstract class BaseModel
    {
        [NotMapped]
        public string ViewType { get; set; }

        [NotMapped]
        public string RenderPage { get; set; }
    }
}
