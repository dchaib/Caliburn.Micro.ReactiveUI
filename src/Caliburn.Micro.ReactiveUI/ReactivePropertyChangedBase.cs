using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Caliburn.Micro.ReactiveUI
{
    public class ReactivePropertyChangedBase : ReactiveObject, INotifyPropertyChangedEx
    {
        public bool IsNotifying
        {
            get;
            set;
        }

        /// <summary>
        ///   Notifies subscribers of the property change.
        /// </summary>
        /// <typeparam name = "TProperty">The type of the property.</typeparam>
        /// <param name = "property">The property expression.</param>
        public virtual void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            raisePropertyChanged(property.GetMemberInfo().Name);
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
            raisePropertyChanged(string.Empty);
        }        
    }
}
