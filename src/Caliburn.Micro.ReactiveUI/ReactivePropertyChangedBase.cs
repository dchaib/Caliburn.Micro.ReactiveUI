using ReactiveUI;
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Caliburn.Micro.ReactiveUI
{
    [DataContract]
    public class ReactivePropertyChangedBase : ReactiveObject, INotifyPropertyChangedEx
    {
        /// <summary>
        /// Enables/Disables property change notification.
        /// Virtualized in order to help with document oriented view models.
        /// </summary>
        public virtual bool IsNotifying
        {
            get { return AreChangeNotificationsEnabled(); }
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
            this.RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Raises a change notification indicating that all bindings should be refreshed.
        /// </summary>
        public virtual void Refresh()
        {
            NotifyOfPropertyChange(string.Empty);
        }
    }
}
