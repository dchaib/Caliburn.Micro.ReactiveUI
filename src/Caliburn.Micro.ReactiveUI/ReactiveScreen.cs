﻿using System.Reflection;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caliburn.Micro.ReactiveUI
{
    /// <summary>
    ///   A base implementation of <see cref = "IScreen" />.
    /// </summary>
    public class ReactiveScreen : ReactiveViewAware, IScreen, IChild
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ReactiveScreen));

        bool isActive;
        bool isInitialized;
        object parent;
        string displayName;

        /// <summary>
        ///   Creates an instance of <see cref="ReactiveScreen"/>.
        /// </summary>
        public ReactiveScreen()
        {
            displayName = GetType().FullName;
        }

        /// <summary>
        ///   Gets or Sets the Parent <see cref = "IConductor" />
        /// </summary>
        public virtual object Parent
        {
            get { return parent; }
            set
            {
                this.RaiseAndSetIfChanged(x => x.Parent, ref parent, value);
            }
        }

        /// <summary>
        ///   Gets or Sets the Display Name
        /// </summary>
        public virtual string DisplayName
        {
            get { return displayName; }
            set
            {
                this.RaiseAndSetIfChanged(x => x.DisplayName, ref displayName, value);
            }
        }

        /// <summary>
        ///   Indicates whether or not this instance is currently active.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            private set
            {
                this.RaiseAndSetIfChanged(x => x.IsActive, ref isActive, value);
            }
        }

        /// <summary>
        ///   Indicates whether or not this instance is currently initialized.
        /// </summary>
        public bool IsInitialized
        {
            get { return isInitialized; }
            private set
            {
                this.RaiseAndSetIfChanged(x => x.IsInitialized, ref isInitialized, value);
            }
        }

        /// <summary>
        ///   Raised after activation occurs.
        /// </summary>
        public event EventHandler<ActivationEventArgs> Activated = delegate { };

        /// <summary>
        ///   Raised before deactivation.
        /// </summary>
        public event EventHandler<DeactivationEventArgs> AttemptingDeactivation = delegate { };

        /// <summary>
        ///   Raised after deactivation.
        /// </summary>
        public event EventHandler<DeactivationEventArgs> Deactivated = delegate { };

        void IActivate.Activate()
        {
            if (IsActive)
            {
                return;
            }

            var initialized = false;

            if (!IsInitialized)
            {
                IsInitialized = initialized = true;
                OnInitialize();
            }

            IsActive = true;
            Log.Info("Activating {0}.", this);
            OnActivate();

            Activated(this, new ActivationEventArgs
            {
                WasInitialized = initialized
            });
        }

        /// <summary>
        ///   Called when initializing.
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        ///   Called when activating.
        /// </summary>
        protected virtual void OnActivate() { }

        void IDeactivate.Deactivate(bool close)
        {
            if (IsActive || (IsInitialized && close))
            {
                AttemptingDeactivation(this, new DeactivationEventArgs
                {
                    WasClosed = close
                });

                IsActive = false;
                Log.Info("Deactivating {0}.", this);
                OnDeactivate(close);

                Deactivated(this, new DeactivationEventArgs
                {
                    WasClosed = close
                });

                if (close)
                {
                    Views.Clear();
                    Log.Info("Closed {0}.", this);
                }
            }
        }

        /// <summary>
        ///   Called when deactivating.
        /// </summary>
        /// <param name = "close">Inidicates whether this instance will be closed.</param>
        protected virtual void OnDeactivate(bool close) { }

        /// <summary>
        ///   Called to check whether or not this instance can close.
        /// </summary>
        /// <param name = "callback">The implementor calls this action with the result of the close check.</param>
        public virtual void CanClose(Action<bool> callback)
        {
            callback(true);
        }

#if WinRT
        System.Action GetViewCloseAction(bool? dialogResult) {
            var conductor = Parent as IConductor;
            if (conductor != null) {
                return () => conductor.CloseItem(this);
            }

            foreach (var contextualView in Views.Values) {
                var viewType = contextualView.GetType();

                var closeMethod = viewType.GetRuntimeMethod("Close", new Type[0]);
                if (closeMethod != null) {
                    return () => { closeMethod.Invoke(contextualView, null); };
                }

                var isOpenProperty = viewType.GetRuntimeProperty("IsOpen");
                if (isOpenProperty != null) {
                    return () => isOpenProperty.SetValue(contextualView, false, null);
                }
            }

            return () => Log.Info("TryClose requires a parent IConductor or a view with a Close method or IsOpen property.");
        }
#else
        System.Action GetViewCloseAction(bool? dialogResult)
        {
            var conductor = Parent as IConductor;
            if (conductor != null)
            {
                return () => conductor.CloseItem(this);
            }

            foreach (var contextualView in Views.Values)
            {
                var viewType = contextualView.GetType();

                var closeMethod = viewType.GetMethod("Close");
                if (closeMethod != null)
                    return () =>
                    {
#if !SILVERLIGHT
                        var isClosed = false;
                        if (dialogResult != null)
                        {
                            var resultProperty = contextualView.GetType().GetProperty("DialogResult");
                            if (resultProperty != null)
                            {
                                resultProperty.SetValue(contextualView, dialogResult, null);
                                isClosed = true;
                            }
                        }

                        if (!isClosed)
                        {
                            closeMethod.Invoke(contextualView, null);
                        }
#else
                        closeMethod.Invoke(contextualView, null);
#endif
                    };

                var isOpenProperty = viewType.GetProperty("IsOpen");
                if (isOpenProperty != null)
                {
                    return () => isOpenProperty.SetValue(contextualView, false, null);
                }
            }

            return () => Log.Info("TryClose requires a parent IConductor or a view with a Close method or IsOpen property.");
        }
#endif

        /// <summary>
        ///   Tries to close this instance by asking its Parent to initiate shutdown or by asking its corresponding view to close.
        /// </summary>
        public void TryClose()
        {
            Execute.OnUIThread(() =>
            {
                var closeAction = GetViewCloseAction(null);
                closeAction();
            });
        }

#if !SILVERLIGHT

        /// <summary>
        /// Closes this instance by asking its Parent to initiate shutdown or by asking it's corresponding view to close.
        /// This overload also provides an opportunity to pass a dialog result to it's corresponding view.
        /// </summary>
        /// <param name="dialogResult">The dialog result.</param>
        public virtual void TryClose(bool? dialogResult)
        {
            Execute.OnUIThread(() =>
            {
                var closeAction = GetViewCloseAction(dialogResult);
                closeAction();
            });
        }

#endif
    }
}

