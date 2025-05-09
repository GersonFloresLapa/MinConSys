using Microsoft.Extensions.DependencyInjection;
using MinConSys.Core.Interfaces.Services;
using MinConSys.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinConSys
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configurar inyección de dependencias
            ServiceProvider = DependencyInjection.ConfigureServices();

            // Iniciar con el formulario de login
            var loginForm = new LoginForm(ServiceProvider.GetRequiredService<ILoginService>());

            // Si el login es exitoso, abre el formulario principal
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainForm(ServiceProvider));
            }
        }
    }
}
