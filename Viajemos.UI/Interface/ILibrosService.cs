using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajemos.DTO;

namespace Viajemos.UI.Interface
{
    public interface ILibrosService
    {
        public Task<List<LibroView>> GetLibrosAsync(System.Threading.CancellationToken token);

        public Task<LibroView> GuardarLibro(LibroView libro, System.Threading.CancellationToken token);

        public Task<LibroView> GetLibroAsync(int IdLibro, System.Threading.CancellationToken token);
    }
}
