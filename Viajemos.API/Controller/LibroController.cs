using DevExpress.Xpo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajemos.DTO;
using ViajemosBK.Modelo;

namespace Viajemos.API.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class LibroController : Microsoft.AspNetCore.Mvc.Controller
    {

        private UnitOfWork _uow;
        public LibroController(UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEditorialAsync()
        {
            var editores = _uow.Query<Editorial>().Select(x => new EditorialView
            {
                Nombre = x.Nombre,
                OID = x.Oid,
                Sede = x.Sede,
            });
            return Ok(await editores.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLibrosAsync()
        {
            var libros = _uow.Query<Libro>().Select(x => new LibroView
            {
                Editorial = x.Editorial != null ? new EditorialView
                {
                    Nombre = x.Editorial.Nombre,
                    Sede = x.Editorial.Sede,
                } : null,
                ISBN = x.ISBN,
                Sinopsis = x.Sinopsis,
                Titulo = x.Título,
                NumeroPaginas = x.NumeroPaginas,
                
            }) ;
            return Ok(await libros.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLibroAsync(int id)
        {
            var libro = await _uow.GetObjectByKeyAsync<Libro>(id);

            if (libro != null)
                return Ok(new LibroView
                {
                    Editorial = libro.Editorial != null ? new EditorialView
                    {
                        Nombre = libro.Editorial.Nombre,
                        Sede = libro.Editorial.Sede,
                    } : null,
                    ISBN = libro.ISBN,
                    Sinopsis = libro.Sinopsis,
                    Titulo = libro.Título,
                    NumeroPaginas = libro.NumeroPaginas,
                });
            else
                return NotFound();

        }

        [HttpPut]
        public async Task<IActionResult> UpdateLibroAsync([FromBody] LibroView libro)
        {
            if (libro == null)
                return BadRequest();
            if(libro.OID<=0)
            {
                ModelState.AddModelError("OID", "No tiene la identificación OID");
            }
            var libroModel = await _uow.GetObjectByKeyAsync<Libro>(libro.OID);
            if (libroModel == null)
                ModelState.AddModelError("Libro", "No existe el libro en la base de datos");

            if(libro.Editorial!=null)
            {
                var edit = await _uow.GetObjectByKeyAsync<Editorial>(libro.Editorial.OID);
                if (edit == null)
                    ModelState.AddModelError("Editorial", "No existe el editorial en la base de datos");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            libroModel.Título = libro.Titulo;
            libroModel.ISBN = libro.ISBN;
            libroModel.NumeroPaginas = libro.NumeroPaginas;
            libroModel.Editorial = libro.Editorial != null ? await _uow.GetObjectByKeyAsync<Editorial>(libro.Editorial.OID) : null;
            libroModel.Sinopsis = libro.Sinopsis;

            await _uow.CommitChangesAsync();

            return NoContent();

            

            
        }

       
    }
}
