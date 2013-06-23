﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caliburn.Micro.ReactiveUI
{
    /// <summary>
    /// A base class for various implementations of <see cref="IConductor"/> that maintain an active item.
    /// </summary>
    /// <typeparam name="T">The type that is being conducted.</typeparam>
    public abstract class ReactiveConductorBaseWithActiveItem<T> : ReactiveConductorBase<T>, IConductActiveItem where T : class
    {
        T activeItem;

        /// <summary>
        /// The currently active item.
        /// </summary>
        public T ActiveItem
        {
            get { return activeItem; }
            set { ActivateItem(value); }
        }

        /// <summary>
        /// The currently active item.
        /// </summary>
        /// <value></value>
        object IHaveActiveItem.ActiveItem
        {
            get { return ActiveItem; }
            set { ActiveItem = (T)value; }
        }

        /// <summary>
        /// Changes the active item.
        /// </summary>
        /// <param name="newItem">The new item to activate.</param>
        /// <param name="closePrevious">Indicates whether or not to close the previous active item.</param>
        protected virtual void ChangeActiveItem(T newItem, bool closePrevious)
        {
            ScreenExtensions.TryDeactivate(activeItem, closePrevious);

            newItem = EnsureItem(newItem);

            if (IsActive)
                ScreenExtensions.TryActivate(newItem);

            activeItem = newItem;
            raisePropertyChanged("ActiveItem");
            OnActivationProcessed(activeItem, true);
        }
    }
}
