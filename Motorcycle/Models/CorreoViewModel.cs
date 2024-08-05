using System.ComponentModel.DataAnnotations;

namespace Motorcycle.Models
{
    public class CorreoViewModel
    {
        
            public string ?Para { get; set; }
            public string? Asunto { get; set; }
            public string? Mensaje { get; set; }
            public IFormFile? Fichero { get; set; }
        

    }
}
