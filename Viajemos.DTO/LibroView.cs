using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Viajemos.DTO
{
    public class LibroView
    {
        public int OID { get; set; }

        public int OIDEditorial { get; set; }

        public int ISBN { get; set; }

        public string Titulo { get; set; }

        public string Sinopsis { get; set; }

        public string NumeroPaginas { get; set; }

        public EditorialView Editorial { get; set; }

        public IEnumerable<AutorView> Autores { get; set; }
    }
}
