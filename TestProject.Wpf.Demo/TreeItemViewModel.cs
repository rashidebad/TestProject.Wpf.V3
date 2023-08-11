// <copyright file="TreeItemViewModel.cs" company="ABC Inc.">
// Copyright (c) ABC Inc. All rights reserved.
// </copyright>

namespace TestProject.Wpf.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    ///     Defines the ViewModel for the items of a Tree.
    /// </summary>
    public class TreeItemViewModel : ViewModel
    {
        private readonly TreeViewModel treeViewModel;

        private bool isHighlighted;

        private bool isPartiallySelected;

        private bool isExpanded;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TreeItemViewModel"/> class.
        /// </summary>
        /// <param name="treeViewModel">
        ///     The <see cref="TreeViewModel"/>.
        /// </param>
        public TreeItemViewModel(TreeViewModel treeViewModel)
        {
            this.treeViewModel = treeViewModel;
        }

        /// <summary>
        ///    Gets or sets the children of the tree item.
        /// </summary>
#pragma warning disable CA2227 // Collection properties should be read only
        public IList<TreeItemViewModel> Children { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        ///     Gets or sets the list indicating whether the tree elements have siblings.
        /// </summary>
#pragma warning disable CA2227 // Collection properties should be read only
        public IList<bool> TreeElements { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        ///     Gets or sets a value indicating whether the item is highlighted.
        /// </summary>
        public bool IsHighlighted
        {
            get => this.isHighlighted;

            set
            {
                this.isHighlighted = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the item is selected.
        /// </summary>
        public bool IsSelected { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether the item is partially selected, which means that at least one of its descendants is selected.
        /// </summary>
        public bool IsPartiallySelected
        {
            get => this.isPartiallySelected;

            internal set
            {
                if (this.isPartiallySelected == value)
                {
                    return;
                }

                this.isPartiallySelected = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the tree item is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get => this.isExpanded;

            set
            {
                if (this.isExpanded == value)
                {
                    return;
                }

                this.isExpanded = value;

                this.treeViewModel.RefreshTree();
            }
        }

        /// <summary>
        ///    Gets a value indicating whether the item has at least one child.
        /// </summary>
        public bool HasChildren => this.Children != null && this.Children.Any();

        /// <summary>
        ///     Gets the parent of this item.
        /// </summary>
        public TreeItemViewModel Parent { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this item has a next sibling.
        /// </summary>
        public bool HasNextSibling { get; internal set; }
    }
}
