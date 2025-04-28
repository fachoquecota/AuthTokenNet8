using System.ComponentModel.DataAnnotations;

namespace Validaciones.Models
{
    public class DBBoolResult
    {
        [Required]
        public bool result { get; set; }
        public string value { get; set; }
    }
}
