/*
 * MAL: Acoplamiento fuerte - los componentes dependen directamente unos de otros
 */

namespace LooseCouplingModularity.Bad
{
    /// <summary>
    /// Implementación concreta de correo electrónico
    /// </summary>
    public class EmailSender
    {
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"[EMAIL] Enviando correo a {to}: {subject}");
        }
    }

    /// <summary>
    /// Fuertemente acoplado a EmailSender
    /// </summary>
    public class OrderProcessor
    {
        private readonly EmailSender _emailSender;

        public OrderProcessor()
        {
            // Dependencia codificada directamente - ¡ACOPLAMIENTO FUERTE!
            _emailSender = new EmailSender();
        }

        public void ProcessOrder(int orderId, string customerEmail)
        {
            Console.WriteLine($"Procesando orden {orderId}...");

            // Lógica de negocio
            Console.WriteLine("  - Validando orden...");
            Console.WriteLine("  - Procesando pago...");
            Console.WriteLine("  - Actualizando inventario...");

            // Notificación - llama directamente a EmailSender
            _emailSender.SendEmail(
                customerEmail,
                $"Orden {orderId} Confirmada",
                "¡Gracias por su compra!"
            );

            Console.WriteLine($"¡Orden {orderId} completada!\n");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var processor = new OrderProcessor();
            processor.ProcessOrder(12345, "cliente@ejemplo.com");

            Console.WriteLine("[X] PROBLEMAS:");
            Console.WriteLine("- OrderProcessor está fuertemente acoplado a EmailSender");
            Console.WriteLine("- No se puede probar OrderProcessor sin enviar correos");
            Console.WriteLine("- No se puede cambiar a SMS sin modificar OrderProcessor");
            Console.WriteLine("- No se puede enviar correo Y SMS simultáneamente");
            Console.WriteLine("- Difícil de probar unitariamente de forma aislada");

            Console.WriteLine("\n[X] ¿Qué pasa si queremos notificaciones por SMS?");
            Console.WriteLine("   → Debemos modificar el código de OrderProcessor");
            Console.WriteLine("   → Debemos crear lógica if/else para el tipo de notificación");
            Console.WriteLine("   → Viola el Principio de Abierto/Cerrado");
        }
    }
}
