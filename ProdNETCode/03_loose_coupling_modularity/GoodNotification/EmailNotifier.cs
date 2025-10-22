/*
 * Implementación de correo electrónico de la interfaz INotifier
 */

namespace LooseCouplingModularity.Good
{
    /// <summary>
    /// Implementación concreta para correo electrónico
    /// </summary>
    public class EmailNotifier : INotifier
    {
        public void Send(string recipient, string subject, string message)
        {
            Console.WriteLine($"[EMAIL] EMAIL a {recipient}");
            Console.WriteLine($"   Asunto: {subject}");
            Console.WriteLine($"   Mensaje: {message}");
        }
    }
}
