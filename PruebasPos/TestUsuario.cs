using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reglas_de_negocio.Services;
using Datos.Models;

namespace PruebasPos
{
    [TestClass]
    public class TestUsuario
    {
        [TestMethod]
        public void IniciarSesionTestEP()
        {
            //Arrange
            string id = "207910029";
            string clave = "123";

            //Act
            ServiceUsuario service = new ServiceUsuario();
            Usuario us= service.GetUsuario(id, clave);
            //Assert
            Assert.IsNotNull(us);
        }

        [TestMethod]
        public void IniciarSesionTestEN()
        {
            //Arrange
            string id = "207910029";
            string clave = "1234";

            //Act
            ServiceUsuario service = new ServiceUsuario();
            Usuario us = service.GetUsuario(id, clave);

            //Assert
            Assert.IsNull(us);
        }
    }
}
