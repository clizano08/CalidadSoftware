using Datos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Datos.Repository
{
    public class RepositoryUsuario
    {
        public Usuario GetUsuario(string id, string password)
        {

            Usuario oUsuario = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.
                                 Where(p => p.Id.Equals(id) && p.Clave == password).
                                 FirstOrDefault<Usuario>();
                }

                if (oUsuario != null)
                    oUsuario = GetUsuarioByID(id);

                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario GetUsuarioByID(string id)
        {
            Usuario usuario = null;
            try
            {
                //string clave = "";
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    usuario = ctx.Usuario.FirstOrDefault<Usuario>();
                }
                //clave = Cryptography.DecrypthAES(usuario.Clave);
                //usuario.Clave = clave;

                return usuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}