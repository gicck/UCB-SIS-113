/*
 * Notificador compuesto - envía a través de múltiples canales
 */

namespace LooseCouplingModularity.Good
{
    /// <summary>
    /// Envía notificaciones a través de múltiples canales
    /// </summary>
    public class MultiNotifier : INotifier
    {
        private readonly List<INotifier> _notifiers;

        public MultiNotifier(List<INotifier> notifiers)
        {
            _notifiers = notifiers;
        }

        public void Send(string recipient, string subject, string message)
        {
            foreach (var notifier in _notifiers)
            {
                notifier.Send(recipient, subject, message);
            }
        }
    }
}
