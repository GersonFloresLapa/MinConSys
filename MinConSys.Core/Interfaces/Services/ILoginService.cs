using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> Autenticacion(string nombreUsuario, string clave);
    }
}
