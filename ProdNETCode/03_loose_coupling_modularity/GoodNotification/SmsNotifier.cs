/*
 * Implementación de SMS de la interfaz INotifier
 */

namespace LooseCouplingModularity.Good
{
    /// <summary>
    /// Implementación concreta para SMS
    /// </summary>
    public class SmsNotifier : INotifier
    {
        public void Send(string recipient, string subject, string message)
        {
            Console.WriteLine($"[SMS] SMS a {recipient}");
            Console.WriteLine($"   {message}");
        }
    }
}
