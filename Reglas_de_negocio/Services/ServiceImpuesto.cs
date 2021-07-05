using Datos.Models;
using Datos.Repository;


namespace Reglas_de_negocio.Services
{
    public class ServiceImpuesto
    {
        public Impuesto GetImpuesto()
        {
            RepositoryImpuesto repository = new RepositoryImpuesto();
            return repository.GetImpuesto();
        }
    }
}
