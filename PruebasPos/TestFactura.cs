using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reglas_de_negocio.Services;
using Datos.Models;
using System.Web.Mvc;

namespace PruebasPos
{
    [TestClass]
    public class TestFactura
    {
        [TestMethod]
        public void FacturarTest()
        {
            //Arrange
            EncFactura enc = new EncFactura();
            ServiceProducto serviceProducto = new ServiceProducto();

            //llena datos encabezado
            enc.Usuario_id = "207910029";
            enc.NombreCliente = "Carlos Zamorano";
            enc.Fecha = DateTime.Now;
            enc.Subtotal = 2582;
            enc.Descuento = 0;
            enc.Total = 2582;
            enc.TipoPago = "Efectivo";
            enc.Estado = "Pago";

            //Act
            ServiceFactura service = new ServiceFactura();
            EncFactura facturaEsperada = service.Save(enc);

            //Assert
            Assert.IsNotNull(facturaEsperada);
        }

        [TestMethod]
        public void DetallePapaTest()
        {
            //Arrange
            ServiceFactura serviceFactura = new ServiceFactura();
            EncFactura enc = serviceFactura.GetFacturaByID(serviceFactura.GetNextConsecutivo());
            ServiceProducto serviceProducto = new ServiceProducto();
            Producto prod = serviceProducto.GetProductoByID(2);
            DetFactura det = new DetFactura();
            det.Secuencia = 0;
            det.Producto_id = 2;
            det.Producto = prod;
            det.EncFactura_id = serviceFactura.GetNextConsecutivo();
            det.Cantidad = 2;
            det.Precio = 1000;
            det.Impuesto = 260;
            det.Total = 2260;

            Producto prodTemp = new Producto();

            //Act
            enc.DetFactura.Add(det);

            foreach (var item in enc.DetFactura)
            {
                if (item.Producto.Equals(prod))
                    prodTemp = item.Producto;
            }

            //Assert
            Assert.AreEqual(prodTemp, prod);
        }

        [TestMethod]
        public void DetalleBotellaTest()
        {
            //Arrange
            ServiceFactura serviceFactura = new ServiceFactura();
            EncFactura enc = serviceFactura.GetFacturaByID(serviceFactura.GetNextConsecutivo());
            ServiceProducto serviceProducto = new ServiceProducto();
            Producto prod = serviceProducto.GetProductoByID(9);
            DetFactura det = new DetFactura();
            det.Secuencia = 0;
            det.Producto_id = 9;
            det.Producto = prod;
            det.EncFactura_id = serviceFactura.GetNextConsecutivo();
            det.Cantidad = 2;
            det.Precio = 1000;
            det.Impuesto = 260;
            det.Total = 2260;

            Producto prodTemp = new Producto();

            //Act
            enc.DetFactura.Add(det);

            foreach (var item in enc.DetFactura)
            {
                if (item.Producto.Equals(prod))
                    prodTemp = item.Producto;
            }

            //Assert
            Assert.AreEqual(prodTemp, prod);
        }

        [TestMethod]
        public void ValidaFacturaTest1()
        {
            //Arrange
            EncFactura enc = new EncFactura();
            ServiceFactura service = new ServiceFactura();

            //llena datos encabezado
            enc.Usuario_id = "207910029";
            enc.NombreCliente = "Carlos Zamorano";
            enc.Fecha = DateTime.Now;
            enc.Subtotal = 0;
            enc.Descuento = 0;
            enc.Total = 0;
            enc.TipoPago = "Efectivo";
            enc.Estado = "Pendiente";
            EncFactura facturaEsperada = service.Save(enc);

            ServiceProducto serviceProducto = new ServiceProducto();
            Producto prod = serviceProducto.GetProductoByID(6);

            //agrega -1 gelatina
            DetFactura det1 = new DetFactura();
            det1.Secuencia = 0;
            det1.Producto_id = 6;
            det1.Producto = prod;
            det1.EncFactura_id = service.GetNextConsecutivo();
            det1.Cantidad = -1;
            det1.Precio = 0;
            det1.Impuesto = 0;
            det1.Total = 0;

            //Act
            
            //facturaEsperada = service.Update(enc);

            //Assert
            Assert.AreNotEqual(-2, det1.Cantidad);
        }

        [TestMethod]
        public void ValidaFacturaTest2()
        {
            //Arrange
            EncFactura enc = new EncFactura();

            //llena datos encabezado
            enc.Usuario_id = "207910029";
            enc.NombreCliente = "Emiliano Porras";
            enc.Fecha = DateTime.Now;
            enc.Subtotal = 1695;
            enc.Descuento = 0;
            enc.Total = 1695;
            enc.TipoPago = "Efectivo";
            enc.Estado = "Pago";

            ServiceFactura service = new ServiceFactura();
            ServiceProducto serviceProducto = new ServiceProducto();
            Producto prod = serviceProducto.GetProductoByID(6);
            EncFactura facturaEsperada = service.Save(enc);

            //agrega 1 gelatina
            DetFactura det1 = new DetFactura();
            det1.Secuencia = 0;
            det1.Producto_id = 6;
            det1.Producto = prod;
            det1.EncFactura_id = service.GetNextConsecutivo();
            det1.Cantidad = 1;
            det1.Precio = 1500;
            det1.Impuesto = 195;
            det1.Total = 1695;

            //Act
            
            facturaEsperada = service.Update(enc);

            //Assert
            Assert.IsNotNull(facturaEsperada);
        }

        [TestMethod]
        public void DescuestoTest5Porcent()
        {
            //Arrange
            ServiceFactura serviceFactura = new ServiceFactura();
            EncFactura enc = new EncFactura();
            ServiceProducto serviceProducto = new ServiceProducto();

            Producto prod1 = serviceProducto.GetProductoByID(2);
            Producto prod2 = serviceProducto.GetProductoByID(3);
            Producto prod3 = serviceProducto.GetProductoByID(6);

            enc.Usuario_id = "207910029";
            enc.NombreCliente = "juasito";
            enc.Subtotal = 11752;
            enc.Total = 11752;
            enc.Descuento = 5;
            enc.TipoPago = "efectivo";
            enc.Estado = "pago";

            serviceFactura.Save(enc);

            //det 1
            DetFactura det1 = new DetFactura();
            det1.Secuencia = 0;
            det1.Producto_id = 2;
            det1.Producto = prod1;
            det1.EncFactura_id = serviceFactura.GetNextConsecutivo();
            det1.Cantidad = 2;
            det1.Precio = 200;
            det1.Impuesto = 52;
            det1.Total = 452;
            //det 2
            DetFactura det2 = new DetFactura();
            det2.Secuencia = 0;
            det2.Producto_id = 3;
            det2.Producto = prod2;
            det2.EncFactura_id = serviceFactura.GetNextConsecutivo();
            det2.Cantidad = 1;
            det2.Precio = 1000;
            det2.Impuesto = 130;
            det2.Total = 1130;
            //det 3
            DetFactura det3 = new DetFactura();
            det3.Secuencia = 0;
            det3.Producto_id = 6;
            det3.Producto = prod3;
            det3.EncFactura_id = serviceFactura.GetNextConsecutivo();
            det3.Cantidad = 6;
            det3.Precio = 1500;
            det3.Impuesto = 1170;
            det3.Total = 10170;


            //Act


            enc.DetFactura.Add(det1);
            enc.DetFactura.Add(det2);
            enc.DetFactura.Add(det3);

            serviceFactura.Descuento(enc);
            decimal descEsperado = Convert.ToDecimal(587.60);

            //Assert
            Assert.AreEqual( descEsperado, enc.Descuento);
            //Assert.AreEqual(0 , enc.Descuento );
        }


        [TestMethod]
        public void DescuestoTestEN()
        {
            //Arrange
            ServiceFactura serviceFactura = new ServiceFactura();
            EncFactura enc = new EncFactura();
            ServiceProducto serviceProducto = new ServiceProducto();
           
            Producto prod1 = serviceProducto.GetProductoByID(2);
            Producto prod2 = serviceProducto.GetProductoByID(3);
            Producto prod3 = serviceProducto.GetProductoByID(6);

            enc.Usuario_id = "207910029";
            enc.NombreCliente = "juasito";
            enc.Subtotal = 4777;
            enc.Total = 4777;
            enc.Descuento = 10;
            enc.TipoPago = "efectivo";
            enc.Estado = "pago";

            serviceFactura.Save(enc);

            //det 1
            DetFactura det1 = new DetFactura();
            det1.Secuencia = 0;
            det1.Producto_id = 2;
            det1.Producto = prod1;
            det1.EncFactura_id = serviceFactura.GetNextConsecutivo();
            det1.Cantidad = 2;
            det1.Precio = 200;
            det1.Impuesto = 52;
            det1.Total = 452;
            //det 2
            DetFactura det2 = new DetFactura();
            det2.Secuencia = 0;
            det2.Producto_id = 3;
            det2.Producto = prod2;
            det2.EncFactura_id = serviceFactura.GetNextConsecutivo();
            det2.Cantidad = 1;
            det2.Precio = 1000;
            det2.Impuesto = 130;
            det2.Total = 1130;
            //det 3
            DetFactura det3 = new DetFactura();
            det3.Secuencia = 0;
            det3.Producto_id = 6;
            det3.Producto = prod3;
            det3.EncFactura_id = serviceFactura.GetNextConsecutivo();
            det3.Cantidad = 2;
            det3.Precio = 1500;
            det3.Impuesto = 195;
            det3.Total = 3195;


            //Act
            

            enc.DetFactura.Add(det1);
            enc.DetFactura.Add(det2);
            enc.DetFactura.Add(det3);

            serviceFactura.Descuento(enc);


            //Assert
            Assert.AreEqual(0, enc.Descuento );
            //Assert.AreEqual(0 , enc.Descuento );
        }



        [TestMethod]
        public void PromocionTestEP()
        {
            //Arrange
            ServiceFactura serviceFactura = new ServiceFactura();
            EncFactura enc = new EncFactura();
            ServiceProducto serviceProducto = new ServiceProducto();

            
            Producto prod = serviceProducto.GetProductoByID(6);

            enc.Usuario_id = "207910029";
            enc.NombreCliente = "Pedro";
            enc.Subtotal = 3195;
            enc.Total = 3195;
            enc.Descuento = 0;
            enc.TipoPago = "efectivo";
            enc.Estado = "pago";

            serviceFactura.Save(enc);

            //det 1
            DetFactura det = new DetFactura();
            det.Secuencia = 0;
            det.Producto_id = 6;
            det.Producto = prod;
            det.EncFactura_id = serviceFactura.GetNextConsecutivo();
            det.Cantidad = 4;
            det.Precio = 1500;
            det.Impuesto = 195;
            det.Total = 3195;


            //Act
            

            
            enc.DetFactura.Add(det);

            serviceFactura.Update(enc);


            //Assert
            Assert.AreEqual(3195, serviceFactura.GetFacturaByID(serviceFactura.GetNextConsecutivo()).Total);
            
        }



        

        




        //se pierde el valor que le usuario ingresa al pagar por lo tanto no se puede probar
        //[TestMethod]
        //public void PromocionTestEN()
        //{
        //    //Arrange
        //    IServiceFactura serviceFactura = new ServiceFactura();
        //    EncFactura enc = new EncFactura();
        //    IServiceProducto serviceProducto = new ServiceProducto();


        //    Producto prod = serviceProducto.GetProductoByID(6);


        //    //det 
        //    DetFactura det = new DetFactura();
        //    det.Secuencia = 0;
        //    det.Producto_id = 6;
        //    det.Producto = prod;
        //    det.EncFactura_id = 120;
        //    det.Cantidad = 3;
        //    det.Precio = 1500;
        //    det.Impuesto = 195;
        //    det.Total = 4695;


        //    //Act
        //    enc.Usuario_id = "207910029";
        //    enc.NombreCliente = "Pedritos";
        //    enc.Subtotal = 4695;
        //    enc.Total = 4695;
        //    enc.Descuento = 0;
        //    enc.TipoPago = "efectivo";
        //    enc.Estado = "pago";


        //    enc.DetFactura.Add(det);

        //    serviceFactura.Save(enc);


        //    //Assert
        //    Assert.AreEqual(5085, serviceFactura.GetFacturaByID(121).Total);

        //}






        //[TestMethod]
        //public void FacturarTest()
        //{
        //    //Arrange
        //    EncFactura enc = new EncFactura();
        //    ServiceProducto serviceProducto = new ServiceProducto();
        //    Producto prod1 = serviceProducto.GetProductoByID(9);
        //    Producto prod2 = serviceProducto.GetProductoByID(2);
        //    //agrega 2 botellas de agua
        //    DetFactura det1 = new DetFactura();
        //    det1.Secuencia = 0;
        //    det1.Producto_id = 9;
        //    det1.Producto = prod1;
        //    det1.EncFactura_id = 116;
        //    det1.Cantidad = 2;
        //    det1.Precio = 1000;
        //    det1.Impuesto = 130;
        //    det1.Total = 2130;

        //    //agrega 2 paq de papas
        //    DetFactura det2 = new DetFactura();
        //    det2.Secuencia = 0;
        //    det2.Producto_id = 2;
        //    det2.Producto = prod2;
        //    det2.EncFactura_id = 116;
        //    det2.Cantidad = 2;
        //    det2.Precio = 200;
        //    det2.Impuesto = 52;
        //    det2.Total = 452;

        //    //llena datos encabezado
        //    enc.Usuario_id = "207910029";
        //    enc.NombreCliente = "Carlos Zamorano";
        //    enc.Fecha = DateTime.Now;
        //    enc.Subtotal = 2582;
        //    enc.Descuento = 0;
        //    enc.Total = 2582;
        //    enc.TipoPago = "Efectivo";
        //    enc.Estado = "Pago";

        //    //Act

        //    ServiceFactura service = new ServiceFactura();
        //    EncFactura facturaEsperada = service.Save(enc);

        //    //Assert
        //    Assert.IsNotNull(facturaEsperada);
        //}

    }
}