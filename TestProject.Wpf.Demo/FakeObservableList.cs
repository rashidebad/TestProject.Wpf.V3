// <copyright file="FakeObservableList.cs" company="ABC Inc.">
// Copyright (c) ABC Inc. All rights reserved.
// </copyright>

namespace TestProject.Wpf
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary>
    ///     Provides a class equivalent to List with a fake INotifyCollectionChanged implementation.
    /// </summary>
    /// <remarks>
    ///     To avoid memory leak in collection binding, the bound collection needs to implement INotifyCollectionChanged.
    ///     This class offers an alternative to the traditional ObservableCollection with better performance when the observability is not needed.
    /// </remarks>
    /// <typeparam name="T">
    ///     The type of elements in the list.
    /// </typeparam>
#pragma warning disable CA1710 // Identifiers should have correct suffix
    public class FakeObservableList<T> : List<T>, INotifyCollectionChanged
#pragma warning restore CA1710 // Identifiers should have correct suffix
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FakeObservableList{T}"/> class.
        /// </summary>
        public FakeObservableList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FakeObservableList{T}"/> class.
        /// </summary>
        /// <param name="capacity">
        ///     The number of elements that the new list can initially store.
        /// </param>
        public FakeObservableList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FakeObservableList{T}"/> class.
        /// </summary>
        /// <param name="collection">
        ///     The collection whose elements are copied to the new list.
        /// </param>
        public FakeObservableList(IEnumerable<T> collection)
            : base(collection)
        {
        }

        /// <summary>
        ///     Occurs when the collection changes, either by adding or removing an item.
        /// </summary>
        /// <remarks>
        ///     See <seealso cref="INotifyCollectionChanged"/>.
        /// </remarks>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        ///     Indicates that the collection has changed.
        /// </summary>
        public void OnCollectionChanged()
        {
            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
