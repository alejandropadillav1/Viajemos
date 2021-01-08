using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Viajemos.DTO
{
    public class LibroView
    {
        public int OID { get; set; }

        public int OIDEditorial { get; set; }

 
        public int ISBN { get; set; }

        [Required(ErrorMessage = "Se requiere el Título")]
        [MaxLength(45, ErrorMessage = "Se requiere el título máximo hasta 45 caracteres")]
        public string Titulo { get; set; }

        [MaxLength(45)]
        public string Sinopsis { get; set; }

        [MaxLength(45)]
        public string NumeroPaginas { get; set; }

      
        public EditorialView Editorial { get; set; }

        public IEnumerable<AutorView> Autores { get; set; }
    }
}
