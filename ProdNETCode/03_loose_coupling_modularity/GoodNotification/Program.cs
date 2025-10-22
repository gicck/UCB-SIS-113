/*
 * BIEN: Sistema débilmente acoplado con inyección de dependencias
 */

namespace LooseCouplingModularity.Good
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Escenario 1: Notificaciones por correo electrónico ===");
            var emailNotifier = new EmailNotifier();
            var processor = new OrderProcessor(emailNotifier);
            processor.ProcessOrder(12345, "cliente@ejemplo.com");

            Console.WriteLine("\n=== Escenario 2: Notificaciones por SMS ===");
            var smsNotifier = new SmsNotifier();
            processor = new OrderProcessor(smsNotifier);
            processor.ProcessOrder(67890, "+1234567890");

            Console.WriteLine("\n=== Escenario 3: Correo Y SMS simultáneamente ===");
            var multiNotifier = new MultiNotifier(new List<INotifier> 
            { 
                emailNotifier, 
                smsNotifier 
            });
            processor = new OrderProcessor(multiNotifier);
            processor.ProcessOrder(11111, "cliente@ejemplo.com / +1234567890");

            Console.WriteLine("\n[OK] BENEFICIOS:");
            Console.WriteLine("- OrderProcessor no sabe ni le importa CÓMO se envía la notificación");
            Console.WriteLine("- Se puede intercambiar fácilmente correo por SMS (o agregar más)");
            Console.WriteLine("- Fácil de probar con un notificador simulado (mock)");
            Console.WriteLine("- Se pueden componer múltiples notificadores");
            Console.WriteLine("- NO hay cambios en OrderProcessor al agregar nuevos tipos de notificación");
            Console.WriteLine("- Cada módulo es independiente y reutilizable");
        }
    }
}
