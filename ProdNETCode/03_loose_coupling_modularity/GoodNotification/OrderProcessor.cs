/*
 * Procesador de órdenes con inyección de dependencias - débilmente acoplado
 */

namespace LooseCouplingModularity.Good
{
    /// <summary>
    /// Débilmente acoplado - depende de la interfaz INotifier
    /// </summary>
    public class OrderProcessor
    {
        private readonly INotifier _notifier;

        public OrderProcessor(INotifier notifier)
        {
            // Dependencia inyectada - ¡ACOPLAMIENTO DÉBIL!
            _notifier = notifier;
        }

        public void ProcessOrder(int orderId, string customerContact)
        {
            Console.WriteLine($"Procesando orden {orderId}...");

            // Lógica de negocio
            Console.WriteLine("  - Validando orden...");
            Console.WriteLine("  - Procesando pago...");
            Console.WriteLine("  - Actualizando inventario...");

            // Notificación - usa el notificador inyectado (¡cualquier implementación!)
            _notifier.Send(
                customerContact,
                $"Orden {orderId} Confirmada",
                "¡Gracias por su compra!"
            );

            Console.WriteLine($"¡Orden {orderId} completada!\n");
        }
    }
}
