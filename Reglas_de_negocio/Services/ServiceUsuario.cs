using Datos.Models;
using Datos.Repository;
using Reglas_de_negocio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reglas_de_negocio.Services
{
    public class ServiceUsuario
    {
        public Usuario GetUsuario(string id, string password)
        {
            RepositoryUsuario repository = new RepositoryUsuario();

            // Se encripta el valor que viene y se compara con el valor encriptado en al BD.
            string crytpPasswd = Cryptography.EncrypthAES(password);
            //int num = 0;
            return repository.GetUsuario(id, crytpPasswd);

        }
        public Usuario GetUsuarioByID(string id)
        {
            RepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarioByID(id);
        }
    }
}