using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Viajemos.DTO
{
    public class AutorView
    {
        public int OID { get; set; }
        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public IEnumerable<LibroView> Libros { get; set; }
    }
}
