using DevExpress.Xpo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViajemosBK.Modelo;

namespace ViajemosBK.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class CargaAleatorioController : Microsoft.AspNetCore.Mvc.Controller
    {
        private UnitOfWork _uow;
        object objeto = new object();

        public CargaAleatorioController(UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> CargaAleatoriaLibrosYAutores()
        {
            var valorConInterlocked = 0;
            var edit1 = new Editorial(_uow);
            edit1.Nombre = "Editorial 1";

            var edit2 = new Editorial(_uow);
            edit2.Nombre = "Editorial 2";

            for (int i = 0; i < 1000; i++)
            {
                Interlocked.Increment(ref valorConInterlocked);
                var libro = new Libro(_uow);
                libro.Título = string.Format("Libro Alejo {0} - Padi {1}", valorConInterlocked, new Random().Next(0, 1000));
                libro.NumeroPaginas = new Random().Next(0, 500).ToString();
                libro.ISBN = valorConInterlocked;
                libro.Sinopsis = string.Format("Descrpicion Alejo {0}", valorConInterlocked);
                var autor1 = new Autor(_uow);
                autor1.Nombre = string.Format("Alejandro {0}", new Random().Next(0,500));
                autor1.Apellidos = "Padilla V";
                libro.Autores.Add(autor1);

                if(valorConInterlocked %2 == 0)
                {
                    libro.Editorial = edit1;
                }
                else
                {
                    libro.Editorial = edit2;
                }
                

                //lock (objeto)
                await _uow.CommitTransactionAsync();

            }

            

            await _uow.CommitChangesAsync();

            return Ok();
        }

    }
}
