using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Services;
using MinConSys.Infrastructure.Data;
using MinConSys.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.DI
{
    public static class DependencyInjection
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Configuración de appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Registrar configuración
            services.AddSingleton<IConfiguration>(configuration);

            // Registrar fábrica de conexiones
            services.AddSingleton<ConnectionFactory>(sp =>
                new ConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

            // Registrar repositorios
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IVehiculoRepository, VehiculoRepository>();
            services.AddScoped<IContratoRepository, ContratoRepository>();
            services.AddScoped<ILocalidadRepository, LocalidadRepository>();
            services.AddScoped<ITablaGeneralesRepository, TablaGeneralesRepository>();
            services.AddScoped<IClaseRepository, ClaseRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IBalanzaRepository, BalanzaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IAdjuntoRepository, AdjuntoRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IRepresentanteRepository, RepresentanteRepository>();
            services.AddScoped<IRumaRepository, RumaRepository>();
            services.AddScoped<ICuentaBancariaRepository, CuentaBancariaRepository>();
            services.AddScoped<ILeyRepository, LeyRepository>();
            // Más repositorios...

            // Registrar servicios de negocio
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPersonaService, PersonaService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IVehiculoService, VehiculoService>();
            services.AddScoped<IContratoService, ContratoService>();
            services.AddScoped<ILocalidadService, LocalidadService>();
            services.AddScoped<ITablaGeneralesService, TablaGeneralesService>();
            services.AddScoped<IClaseService, ClaseService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IBalanzaService, BalanzaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IAdjuntoService, AdjuntoService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IRepresentanteService, RepresentanteService>();
            services.AddScoped<IRumaService, RumaService>();
            services.AddScoped<ICuentaBancariaService, CuentaBancariaService>();
            services.AddScoped<ILeyService, LeyService>();
            // Más servicios...

            return services.BuildServiceProvider();
        }
    }
}
