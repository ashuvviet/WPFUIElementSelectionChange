using System.Windows;

namespace WpfApp2
{
    internal class ExecuteCommandOnRoutedEventBehavior
    {
        private RoutedEvent routedEvent;

        public ExecuteCommandOnRoutedEventBehavior(RoutedEvent routedEvent)
        {
            this.routedEvent = routedEvent;
        }
    }
}