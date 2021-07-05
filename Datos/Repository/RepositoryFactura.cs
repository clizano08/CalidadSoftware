using Datos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Datos.Repository
{
    public class RepositoryFactura
    {
        public void DeleteFactura(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EncFactura> GetFactura()
        {
            throw new NotImplementedException();
        }


        public EncFactura GetFacturaByID(int id)
        {
            int consecutivo = id;
            EncFactura factura = null;
            try
            {


                // Forma No 1
                using (MyContext ctx = new MyContext())
                {
                    // Siempre desactivar LazyLoading
                    ctx.Configuration.LazyLoadingEnabled = false;

                    // REvisar el Modelo y ver las relaciones con Include las vincula
                     factura = ctx.EncFactura.
                               Include("Usuario").
                               Include("DetFactura").
                               Include("DetFactura.Producto").
                               Where(p => p.Id == id).
                               FirstOrDefault<EncFactura>();
                    


                }
                return factura;

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
                throw new Exception(mensaje);
            }
        }



        public EncFactura GetSecuenciaFactura(int id)
        {
            int consecutivo = id;
            EncFactura factura = null;
            try
            {


                // Forma No 1
                using (MyContext ctx = new MyContext())
                {
                    // Siempre desactivar LazyLoading
                    ctx.Configuration.LazyLoadingEnabled = false;

                    // REvisar el Modelo y ver las relaciones con Include las vincula
                    factura = ctx.EncFactura.
                               Include("Usuario").
                               Include("DetFactura").
                               Include("DetFactura.Producto").
                               Where(p => p.Id == consecutivo+1).
                               FirstOrDefault<EncFactura>();



                    if (factura == null)
                    {
                        factura = ctx.EncFactura.
                               Include("Usuario").
                               Include("DetFactura").
                               Include("DetFactura.Producto").
                               FirstOrDefault<EncFactura>();
                    }


                }
                return factura;

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
                throw new Exception(mensaje);
            }
        }

        public EncFactura Save(EncFactura pFacturaEncabezado)
        {
            int resultado = 0;
            EncFactura factura = null;
            try
            {
      
                int NoFactura = GetNextConsecutivo();
          
                pFacturaEncabezado.Estado = "Pago";

                
                // Asignar Consecutivo en el Detalle.
                foreach (var detalle in pFacturaEncabezado.DetFactura)
                {
                    detalle.EncFactura_id = NoFactura;
                    
                    

                }

                
                using (MyContext ctx = new MyContext())
                {
                    using (var transaccion = ctx.Database.BeginTransaction())
                    {
                     

                        ctx.EncFactura.Add(pFacturaEncabezado);
                        
                 
                        resultado = ctx.SaveChanges();

                        // Commit 
                        transaccion.Commit();
                    }
                }

                // Buscar la factura que se salvó y reenviarla
                if (resultado >= 0)
                    factura = GetSecuenciaFactura(NoFactura);

                return factura;
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
                throw new Exception(mensaje);
            }

        }

        public EncFactura Update(EncFactura facturaEncabezado)
        {
            int resultado = 0;
            EncFactura factura = null;
            try
            {
                int NoFactura = GetNextConsecutivo();

                foreach (var detalle in facturaEncabezado.DetFactura)
                {
                    detalle.EncFactura_id = NoFactura;



                }


                using (MyContext ctx = new MyContext())
                {
                    using (var transaccion = ctx.Database.BeginTransaction())
                    {


                        ctx.Entry(facturaEncabezado).State = EntityState.Modified;


                        resultado = ctx.SaveChanges();

                        // Commit 
                        transaccion.Commit();
                    }
                }

                // Buscar la factura que se salvó y reenviarla
                if (resultado >= 0)
                    factura = GetSecuenciaFactura(NoFactura-1);

                return factura;
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
                throw new Exception(mensaje);
            }

        }   
        
        /// <summary>
        /// Extraer un consecutivo para asignar el numero de factura
        ///  Investigue como crear secuencias en SQLServer
        /// http://technet.microsoft.com/es-es/library/ff878091.aspx
        /// CREATE SEQUENCE SecuenciaNoFactura  START WITH 1 INCREMENT BY 1 ;
        /// </summary>
        /// <returns>Num. de factura</returns>
        public int GetNextConsecutivo()
        {
            int next = 0;
            using (MyContext ctx = new MyContext())
            {
                var siguiente = ctx.Database.SqlQuery<Decimal?>("SELECT MAX(Id) FROM EncFactura")
                                        .FirstOrDefault();

                if (siguiente != null)
                    next = Convert.ToInt16(siguiente);
                else
                    ctx.Database.SqlQuery<string>("DBCC CHECKIDENT (EncFactura, RESEED,0)");
            }

            return next;
        }

        private int GetNextConsecutivoWithAdoCommands()
        {

            string sql = string.Format("SELECT NEXT VALUE FOR SequenceNoFactura");

            DataTable dataTable = new DataTable();
            using (var ctx = new MyContext())
            {
                using (var conn = ctx.Database.Connection)
                {
                    DbConnection connection = ctx.Database.Connection;
                    DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
                    using (var cmd = dbFactory.CreateCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = sql;
                        using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                        {
                            adapter.SelectCommand = cmd;
                            adapter.Fill(dataTable);
                        }
                    }
                }

                return (int)dataTable.Rows[0][0];
            }

        }
    }
}


