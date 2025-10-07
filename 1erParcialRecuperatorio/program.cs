// Program.cs - ESTE CÓDIGO DEBE FUNCIONAR CON TU IMPLEMENTACIÓN
using System;

namespace SistemaBancario
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA BANCARIO - EVALUACIÓN DE ENCAPSULAMIENTO ===\n");
            
            // CASO 1: Validaciones de edad y políticas
            TestValidacionesBasicas();
            
            // CASO 2: Protección de datos financieros
            TestProteccionDatos();
            
            // CASO 3: Límites y restricciones bancarias
            TestLimitesYRestricciones();
            
            // CASO 4: Operaciones controladas y auditoría
            TestOperacionesControladas();
            
            Console.WriteLine("\n=== FIN DE EVALUACIÓN ===");
            Console.ReadKey();
        }

        static void TestValidacionesBasicas()
        {
            Console.WriteLine("--- CASO 1: Validaciones de Políticas Bancarias ---");
            
            var banco = new SistemaBancario();
            
            // Cliente mayor de edad
            var clienteAdulto = banco.RegistrarCliente("María González", "12345678", 
                                                     new DateTime(1990, 5, 15), 3000m);
            
            // Cliente menor de edad
            var clienteMenor = banco.RegistrarCliente("Pedro Joven", "87654321", 
                                                    new DateTime(2010, 3, 20), 0m);
            
            // Intentar abrir cuenta corriente siendo menor (debe fallar)
            bool cuentaCorrienteMenor = banco.AbrirCuenta(clienteMenor.Id, TipoCuenta.Corriente, 100m);
            Console.WriteLine($"Cuenta corriente para menor (debe ser False): {cuentaCorrienteMenor}");
            
            // Abrir cuenta de ahorros para menor (debe funcionar)
            bool cuentaAhorrosMenor = banco.AbrirCuenta(clienteMenor.Id, TipoCuenta.Ahorros, 50m);
            Console.WriteLine($"Cuenta ahorros para menor: {cuentaAhorrosMenor}");
            
            Console.WriteLine();
        }

        static void TestProteccionDatos()
        {
            Console.WriteLine("--- CASO 2: Protección de Datos Financieros ---");
            
            var banco = new SistemaBancario();
            var cliente = banco.RegistrarCliente("Carlos Security", "11111111", 
                                                new DateTime(1985, 8, 10), 5000m);
            
            banco.AbrirCuenta(cliente.Id, TipoCuenta.Ahorros, 1000m);
            var cuenta = cliente.ObtenerCuentas()[0];
            
            // Mostrar información básica (sin datos sensibles)
            Console.WriteLine("=== Información Pública ===");
            cliente.MostrarPerfilBasico();
            cuenta.MostrarInformacionBasica();
            
            // El saldo solo debe ser accesible a través de método controlado
            Console.WriteLine($"Saldo a través de método seguro: ${cuenta.ConsultarSaldo():F2}");
            
            // Intentar acceso directo al saldo debe estar bloqueado por encapsulamiento
            // cuenta.saldo = 999999; // Esto NO debe ser posible
            
            Console.WriteLine();
        }

        static void TestLimitesYRestricciones()
        {
            Console.WriteLine("--- CASO 3: Límites y Restricciones Bancarias ---");
            
            var banco = new SistemaBancario();
            var cliente = banco.RegistrarCliente("Ana Límites", "22222222", 
                                                new DateTime(1988, 12, 5), 4000m);
            
            banco.AbrirCuenta(cliente.Id, TipoCuenta.Ahorros, 2000m);
            var cuentaAhorros = cliente.ObtenerCuentas()[0];
            
            // Probar límite de retiro diario ($500 para ahorros)
            Console.WriteLine("Probando límites de retiro diario:");
            bool retiro1 = cuentaAhorros.Retirar(300m);
            Console.WriteLine($"Retiro $300: {retiro1}");
            
            bool retiro2 = cuentaAhorros.Retirar(250m);
            Console.WriteLine($"Retiro $250 (excede límite): {retiro2}");
            
            // Probar sobregiro en cuenta de ahorros (debe fallar)
            bool retiroExcesivo = cuentaAhorros.Retirar(5000m);
            Console.WriteLine($"Retiro que causa saldo negativo en ahorros: {retiroExcesivo}");
            
            // Abrir cuenta corriente y probar sobregiro permitido
            banco.AbrirCuenta(cliente.Id, TipoCuenta.Corriente, 500m);
            var cuentaCorriente = cliente.ObtenerCuentas()[1];
            
            bool sobregiroPermitido = cuentaCorriente.Retirar(1200m); // -$700 (dentro del límite)
            Console.WriteLine($"Sobregiro de $700 en corriente: {sobregiroPermitido}");
            
            bool sobregiroExcesivo = cuentaCorriente.Retirar(500m); // Excedería -$1000
            Console.WriteLine($"Sobregiro excesivo: {sobregiroExcesivo}");
            
            Console.WriteLine();
        }

        static void TestOperacionesControladas()
        {
            Console.WriteLine("--- CASO 4: Operaciones Controladas ---");
            
            var banco = new SistemaBancario();
            var cliente1 = banco.RegistrarCliente("Luis Transfer", "33333333", 
                                                 new DateTime(1992, 4, 18), 3500m);
            var cliente2 = banco.RegistrarCliente("Rosa Receiver", "44444444", 
                                                 new DateTime(1987, 9, 25), 2800m);
            
            banco.AbrirCuenta(cliente1.Id, TipoCuenta.Corriente, 1500m);
            banco.AbrirCuenta(cliente2.Id, TipoCuenta.Ahorros, 800m);
            
            var cuentaOrigen = cliente1.ObtenerCuentas()[0];
            var cuentaDestino = cliente2.ObtenerCuentas()[0];
            
            // Transferencia válida
            bool transferencia = banco.RealizarTransferencia(cuentaOrigen.NumeroCuenta, 
                                                           cuentaDestino.NumeroCuenta, 300m);
            Console.WriteLine($"Transferencia de $300: {transferencia}");
            
            // Bloquear cuenta y intentar operaciones
            cuentaOrigen.BloquearCuenta();
            bool operacionBloqueada = cuentaOrigen.Retirar(100m);
            Console.WriteLine($"Retiro con cuenta bloqueada: {operacionBloqueada}");
            
            // Mostrar límite de cuentas por cliente
            Console.WriteLine("Intentando abrir 6ta cuenta (límite es 5):");
            for (int i = 2; i <= 6; i++)
            {
                bool nuevaCuenta = banco.AbrirCuenta(cliente1.Id, TipoCuenta.Ahorros, 100m);
                Console.WriteLine($"Cuenta #{i}: {(nuevaCuenta ? "✓ Creada" : "✗ Rechazada")}");
            }
            
            Console.WriteLine();
        }
    }

    public enum TipoCuenta
    {
        Ahorros,
        Corriente,
        Inversion
    }
}