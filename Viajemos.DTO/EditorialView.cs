using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Viajemos.DTO
{
    public class EditorialView
    {
        public int OID { get; set; }

        public string Nombre { get; set; }
        public string Sede { get; set; }

        public IEnumerable<LibroView> Libros { get; set; }
    }
}
