using DevExpress.Xpo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajemos.DTO;
using ViajemosBK.Modelo;

namespace ViajemosBK.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class BibliotecaController : Microsoft.AspNetCore.Mvc.Controller
    {

        private UnitOfWork _uow;

        public BibliotecaController(UnitOfWork uow)
        {
            _uow = uow;
        }


        /// <summary>
        /// De acuerdo al método No 1 Traer Autores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AutorView>> TraerAutoresAsync()
        {
            var autores = _uow.Query<Autor>().Select(a => new AutorView
            {
                Apellidos = a.Apellidos,
                Libros = a.Libros.Any() ? a.Libros.Select(l => new LibroView
                {
                    Editorial = l.Editorial != null ? new EditorialView
                    {
                        Nombre = l.Editorial.Nombre,
                        OID = l.Editorial.Oid,
                        Sede = l.Editorial.Sede
                       
                    } : null,
                    ISBN = l.ISBN,
                    NumeroPaginas = l.NumeroPaginas,
                    Sinopsis = l.Sinopsis,
                    OID = l.Oid,
                    Titulo = l.Título
                }) : null,
                OID = a.Oid,
                Nombre = a.Nombre
            });

            return await autores.ToListAsync();
        }


        /// <summary>
        /// De acuerdo al método No 2 Traer Autores por Editorial
        /// </summary>
        /// <param name="OidEditorial"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AutorView>> TraerAutoresPorEditorial(int OidEditorial)
        {
            try
            {
                var editorial = await _uow.GetObjectByKeyAsync<Editorial>(OidEditorial);

                if (editorial == null)
                    return null;


                var autoresLibros = editorial.Libros.Select(x => x.Autores);

                var tareas = new List<Task>();

                var listaAutorView = new List<AutorView>();

                //Se hará el trabajo en forma Task en forma paralelismo por temas de performance.
                for (int i = 0; i < autoresLibros.Count(); i++)
                {
                    var autorLibro = autoresLibros.ElementAt(i);
                    var respuestaTask = ProcesarAutor(autorLibro, listaAutorView);
                    tareas.Add(respuestaTask);
                }

                //Esperar hasta que termine el task.
                await Task.WhenAll(tareas);

                //Obtener los autores únicos (Distinct)
                var listaAutorViewDistinct = listaAutorView.DistinctBy(x => x.OID);


                return listaAutorViewDistinct.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task ProcesarAutor(XPCollection<Autor> autores, List<AutorView> listaAutorView)
        {
            foreach (var autor in autores)
            {
                var autorView = new AutorView
                {
                    Apellidos = autor.Apellidos,
                    Nombre = autor.Nombre,
                    OID = autor.Oid
                };
                listaAutorView.Add(autorView);
            }
        }


        /// <summary>
        /// De acuerdo al método No 3 Guardar Editorial.
        /// </summary>
        /// <param name="editorialView"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GuardarEditorial(EditorialView editorialView)
        {
            var model = new Editorial(_uow);


            if (!TryValidateModel(editorialView))
                return BadRequest(GetFullErrorMessage(ModelState));

            await CopiarModeloVista(editorialView, model);

            await _uow.CommitChangesAsync();

            return Json(new
            {
                model.Oid
            });
        }

        private async Task CopiarModeloVista(EditorialView viewModel, Editorial modelo)
        {
            modelo.Nombre = viewModel.Nombre;
            modelo.Sede = viewModel.Sede;
            modelo.Oid = viewModel.OID;
        }
        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }

    }
}
