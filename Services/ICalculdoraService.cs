using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Transportes_MVC.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICalculdoraService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICalculdoraService
    {
        [OperationContract]
        double sumar(double a, double b);

        [OperationContract]
        double restar(double a, double b);

        [OperationContract]
        double multiplicar(double a, double b);

        [OperationContract]
        double dividir(double a, double b);
    }
}
