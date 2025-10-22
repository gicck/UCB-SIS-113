/*
 * Interfaz abstracta para notificaciones - habilita el acoplamiento débil
 */

namespace LooseCouplingModularity.Good
{
    /// <summary>
    /// Interfaz base abstracta - define el contrato
    /// </summary>
    public interface INotifier
    {
        /// <summary>
        /// Envía una notificación
        /// </summary>
        void Send(string recipient, string subject, string message);
    }
}
