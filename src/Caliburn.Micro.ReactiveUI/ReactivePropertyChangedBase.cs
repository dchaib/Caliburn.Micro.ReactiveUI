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
        public bool IsNotifying
        {
            get { return areChangeNotificationsEnabled; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <typeparam name = "TProperty">The type of the property.</typeparam>
        /// <param name = "property">The property expression.</param>
        public virtual void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            NotifyOfPropertyChange(property.GetMemberInfo().Name);
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name = "propertyName">Name of the property.</param>
        public virtual void NotifyOfPropertyChange([CallerMemberName] string propertyName = null)
        {
            raisePropertyChanged(propertyName);
        }

        public void Refresh()
        {
            NotifyOfPropertyChange(string.Empty);
        }
    }
}
