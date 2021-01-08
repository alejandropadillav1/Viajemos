using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Viajemos.DTO;
using Viajemos.UI.Interface;

namespace Viajemos.UI.Services
{
    public class LibroService : ILibrosService
    {

        private readonly HttpClient _httpClient;
        public AppSettings _appSettings { get; }

        public LibroService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            httpClient.BaseAddress = new Uri(_appSettings.APIURLBaseAddress);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");

            _httpClient = httpClient;
        }
        public async Task<LibroView> GetLibroAsync(int IdLibro, CancellationToken token)
        {
            return await JsonSerializer.DeserializeAsync<LibroView>(await _httpClient.GetStreamAsync($"api/libro/GetLibro/{IdLibro}"),
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }

        public async Task<List<LibroView>> GetLibrosAsync(CancellationToken token)
        {
            return await JsonSerializer.DeserializeAsync<List<LibroView>>(await _httpClient.GetStreamAsync($"api/libro/GetAllLibros"),
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }

        public async Task ActualizarLibro(LibroView libro, CancellationToken token)
        {
            var libroJson = new StringContent(JsonSerializer.Serialize(libro), Encoding.UTF8, "application/json");

            if (libro.OID > 0)
                await _httpClient.PutAsync("api/libro/UpdateLibro", libroJson);
        }

        public async Task<List<EditorialView>> GetEditorialesAsync(CancellationToken token)
        {
            return await JsonSerializer.DeserializeAsync<List<EditorialView>>(await _httpClient.GetStreamAsync($"api/libro/GetAllEditorial"),
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }
    }
}
