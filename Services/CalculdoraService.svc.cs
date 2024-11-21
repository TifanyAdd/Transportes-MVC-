using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Transportes_MVC.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CalculdoraService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CalculdoraService.svc o CalculdoraService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CalculdoraService : ICalculdoraService
    {
        public double sumar(double a, double b)
        {
            return a + b;
        }

        public double restar(double a, double b)
        {
            return a - b;
        }

        public double multiplicar(double a, double b)
        {
            return a * b;
        }

        public double dividir(double a, double b)
        {
            return a / b;
        }
    }
}
