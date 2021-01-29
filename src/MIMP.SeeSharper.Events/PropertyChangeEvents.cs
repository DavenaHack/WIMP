using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MIMP.SeeSharper.Events
{
    public static class PropertyChangeEvents
    {

        public static void RaisePropertyChanging(this INotifyPropertyChanging sender, [CallerMemberName] string propertyName = null) =>
            Events.InvokeEvent(sender, nameof(INotifyPropertyChanging.PropertyChanging), new PropertyChangingEventArgs(propertyName));

        public static void RaisePropertyChanged(this INotifyPropertyChanged sender, [CallerMemberName] string propertyName = null) =>
            Events.InvokeEvent(sender, nameof(INotifyPropertyChanged.PropertyChanged), new PropertyChangingEventArgs(propertyName));

        public static void RaiseCollectionChanged(this INotifyCollectionChanged sender, NotifyCollectionChangedEventArgs eventArgs) =>
            Events.InvokeEvent(sender, nameof(INotifyCollectionChanged.CollectionChanged), eventArgs);

    }
}
