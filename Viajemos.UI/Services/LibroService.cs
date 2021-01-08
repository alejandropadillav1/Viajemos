using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Viajemos.DTO;
using Viajemos.UI.Interface;

namespace Viajemos.UI.Services
{
    public class LibroService : ILibrosService
    {
        public Task<LibroView> GetLibroAsync(int IdLibro, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<List<LibroView>> GetLibrosAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<LibroView> GuardarLibro(LibroView libro, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
