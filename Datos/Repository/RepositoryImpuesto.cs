using Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repository
{
    public class RepositoryImpuesto
    {
        public Impuesto GetImpuesto()
        {
            Impuesto oImpuesto = null;
            using (MyContext ctx = new MyContext())
            {
                oImpuesto = ctx.Impuesto.FirstOrDefault(); 
            }

            return oImpuesto;
        }
    }
}
