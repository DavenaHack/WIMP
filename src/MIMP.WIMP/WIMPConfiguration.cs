using MIMP.SeeSharper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MIMP.WIMP
{
    public class WIMPConfiguration : ICloneable<WIMPConfiguration>, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        private Grid _defaultGrid;
        public Grid DefaultGrid
        {
            get => _defaultGrid;
            set
            {
                if (_defaultGrid == value)
                    return;
                _defaultGrid = value;
                RaisePropertyChanged();
            }
        }


        private IDictionary<string, Grid> _screenGrid;
        public IDictionary<string, Grid> ScreenGrid
        {
            get => _screenGrid ?? (ScreenGrid = new Dictionary<string, Grid>());
            set
            {
                if (_screenGrid == value)
                    return;
                _screenGrid = value;
                RaisePropertyChanged();
            }
        }


        public Grid Grid(string screen)
        {
            if (ScreenGrid.TryGetValue(screen, out var grid))
                return grid;
            return DefaultGrid;
        }


        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WIMPConfiguration Clone()
        {
            return new WIMPConfiguration
            {
                DefaultGrid = DefaultGrid.Clone(),
                ScreenGrid = ScreenGrid.ToDictionary(x => x.Key, x => x.Value.Clone()),
            };
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }

}
