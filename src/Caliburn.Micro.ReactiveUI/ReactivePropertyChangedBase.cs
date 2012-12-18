using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caliburn.Micro.ReactiveUI
{
    public class ReactivePropertyChangedBase : ReactiveObject, INotifyPropertyChangedEx
    {
        public bool IsNotifying
        {
            get;
            set;
        }

        public void NotifyOfPropertyChange(string propertyName)
        {
            raisePropertyChanged(propertyName);
        }

        public void Refresh()
        {
            raisePropertyChanged(string.Empty);
        }        
    }
}
