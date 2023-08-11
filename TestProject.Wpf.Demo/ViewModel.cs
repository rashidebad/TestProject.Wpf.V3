// <copyright file="ViewModel.cs" company="ABC Inc.">
// Copyright (c) ABC Inc. All rights reserved.
// </copyright>

namespace TestProject.Wpf.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///     Base class for view models.
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        internal virtual void SetView(object view)
        {
        }

        /// <summary>
        ///     Sets the property and calls <see cref="OnPropertyChanged"/> method.
        /// </summary>
        /// <param name="storage">
        ///     Reference to a property with both getter and setter.
        /// </param>
        /// <param name="value">
        ///     Desired value for the property.
        /// </param>
        /// <param name="callerPropertyName">
        ///     Name of the property used to notify listeners. This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <param name="propertyNames">
        ///     Optional additional property names to notify listeners.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the desired value.
        /// </returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string callerPropertyName = null, params string[] propertyNames)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;

            this.OnPropertyChanged(callerPropertyName);
            foreach (var propertyName in propertyNames)
            {
                this.OnPropertyChanged(propertyName);
            }

            return true;
        }

        /// <summary>
        ///     This handler is called to indicate that one of the properties in the ViewModel has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property which was updated; if no value is provided, the name of property will be extracted using the runtime.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
