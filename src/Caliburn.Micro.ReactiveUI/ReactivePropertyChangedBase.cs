using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Caliburn.Micro.ReactiveUI
{
    public class ReactivePropertyChangedBase : ReactiveObject, INotifyPropertyChangedEx
    {
#if !WinRT
        [Browsable(false)]
#endif
        public bool IsNotifying
        {
            get { return areChangeNotificationsEnabled; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///   Notifies subscribers of the property change.
        /// </summary>
        /// <param name = "propertyName">Name of the property.</param>
#if WinRT || NET45
        public virtual void NotifyOfPropertyChange([CallerMemberName]string propertyName = "")
#else
        public virtual void NotifyOfPropertyChange(string propertyName)
#endif
        {
            raisePropertyChanged(propertyName);
        }

        public void Refresh()
        {
            NotifyOfPropertyChange(string.Empty);
        }        
    }
}
