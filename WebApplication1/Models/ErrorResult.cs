using System.ComponentModel.DataAnnotations;

namespace Validaciones.Models
{
    public class ErrorResult
    {
        [Required]
        public static string? ErrorMessage {get; set;}
    }
}
