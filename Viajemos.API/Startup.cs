using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViajemosBK.Modelo;

namespace ViajemosBK
{
    /// <summary>
    /// El proyecto está configurado con .NET 5, que es el futuro de .NET Core, de igual forma, el proyecto funciona sin ningún problema con .NET Core 3.1.
    /// Se puede realizar cambio en la configuración del proyecto que apunten de .NET 5 a .NET Core 3.1.
    /// </summary>
    public class Startup
    {
        public IConfiguration _Configuracion;
        public Startup(IConfiguration Configuracion)
        {
            _Configuracion = Configuracion;
        }
        /// <summary>
        /// Configurar el servicio con ORM - XPO en vez de EF, ver más información sobre benchmarking
        /// https://github.com/DevExpress/XPO/tree/master/Benchmarks
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Para activar el MVC (Controllers)
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            //La cadena de conexión se puede cambiar dentro del archivo appsettings.json, ConfigurationString
            services.AddXpoDefaultUnitOfWork(true, options => options.UseConnectionString(_Configuracion.GetConnectionString("ConnectionString"))
            .UseThreadSafeDataLayer(true)
            .UseAutoCreationOption(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema)
            .UseEntityTypes(typeof(Autor), typeof(Editorial), typeof(Libro)));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "V1",
                    Title = "Viajemos.com",
                    Description = "Services REST Libros",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "alejandropadillav@yahoo.es",
                        Name = "Alejandro Padilla Valderrama",
                        Url = new Uri($"https://github.com/alejandropadillav1")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Alejandro Padilla's license, Opensource, skill test for viajemos.com"
                    }
                });
            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //Configurar el servicio SwaggerUI.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Viajemos.com Libros");
            });

            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
