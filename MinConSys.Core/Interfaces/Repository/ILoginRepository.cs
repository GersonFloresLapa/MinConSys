using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface ILoginRepository
    {
        Task<LoginResponse> Autenticar(string nombreUsuario, string clave);
    }
}
