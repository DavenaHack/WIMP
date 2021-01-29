using MIMP.Window;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MIMP.WIMP
{

    public class ObservableScreenGrids : IReadOnlyDictionary<IScreen, Grid>, INotifyPropertyChanged, INotifyCollectionChanged // TODO listen to events and raise own events
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public event NotifyCollectionChangedEventHandler CollectionChanged;


        public WIMPConfiguration Configuration { get; }

        public IScreens Screens { get; }


        public IEnumerable<IScreen> Keys => Screens;

        public IEnumerable<Grid> Values => Screens.Select(x => this[x]);

        public int Count => Screens.Count;


        public ObservableScreenGrids(IScreens screens, WIMPConfiguration configuration)
        {
            Screens = screens;
            Screens.Connect += (_, e) =>
            {
                e.Screen.PropertyChanged += Screen_PropertyChanged;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, e.Screen));
            };
            foreach (var s in Screens)
                s.PropertyChanged += Screen_PropertyChanged;
            Screens.Disconnect += (_, e) =>
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, e.Screen));
            Configuration = configuration;
        }


        public Grid this[IScreen key] => Configuration.Grid(key.ID);


        public bool ContainsKey(IScreen key)
        {
            return Screens.Contains(key);
        }

        public bool TryGetValue(IScreen key, [MaybeNullWhen(false)] out Grid value)
        {
            value = null;
            if (!ContainsKey(key))
                return false;
            value = this[key];
            return true;
        }


        public IEnumerator<KeyValuePair<IScreen, Grid>> GetEnumerator()
        {
            return Screens.ToDictionary(x => x, x => this[x]).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private void Screen_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"{nameof(Screens)}[{Screens.ToList().IndexOf(sender as IScreen)}].{e.PropertyName}"));
        }

    }
}
