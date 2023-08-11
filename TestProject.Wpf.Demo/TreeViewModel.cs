// <copyright file="TreeViewModel.cs" company="ABC Inc.">
// Copyright (c) ABC Inc. All rights reserved.
// </copyright>

namespace TestProject.Wpf.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    ///     Defines the ViewModel for Tree. Can be used for List too.
    /// </summary>
    public class TreeViewModel : ViewModel
    {
        /// <summary>
        ///     This is the non-public SetSelectedItems() method from <see cref="ListBox"/>.
        /// </summary>
        /// <remarks>
        ///     A protected method of the Selector class is used for performance reasons.
        ///
        ///     Important!!!
        ///     ------------
        ///     This is only intended to be used with <see cref="ListBox"/>es that have a
        ///     <see cref="ListBox.SelectionMode"/> that is not <see cref="SelectionMode.Single"/>;
        ///     it does not properly support those single selection cases and can result in a crash.
        /// </remarks>
        internal static readonly MethodInfo SetSelectedItemsMethod = typeof(ListBox).GetMethod("SetSelectedItems", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new InvalidOperationException("Cannot extract required protected method!");

        private ListBox listBox;

        private bool isSelecting;

        /// <summary>
        ///     Gets or sets the select command.
        /// </summary>
        /// <remarks>
        ///     The select command is executed whether the selection changed through the View (by clicking on the UI) or through the ViewModel (by calling the <see cref="Select"/> method).
        ///     A boolean is passed to the command as a parameter to indicate whether the selection changed through the View (<see langword="true"/>) or through the ViewModel (<see langword="false"/>).
        /// </remarks>
        public ICommand SelectCommand { get; set; }

        /// <summary>
        ///     Gets the list of all tree items.
        /// </summary>
        public IEnumerable<TreeItemViewModel> AllItems => this.GetAllTreeItemViewModels(this.FirstLevel);

        /// <summary>
        ///     Gets the list of all visible items.
        /// </summary>
        public IEnumerable<TreeItemViewModel> VisibleItems { get; private set; } = new FakeObservableList<TreeItemViewModel>();

        /// <summary>
        ///     Gets the list of selected items.
        /// </summary>
        public IEnumerable<TreeItemViewModel> SelectedItems => this.AllItems.Where(x => x.IsSelected);

        /// <summary>
        ///     Gets or sets the first level of the tree.
        /// </summary>
#pragma warning disable CA2227 // Collection properties should be read only
        public IList<TreeItemViewModel> FirstLevel { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        ///     Selects the items in the view.
        /// </summary>
        /// <remarks>
        ///     This method executes the <see cref="SelectCommand"/>.
        /// </remarks>
        /// <param name="selection">
        ///     The items to be selected.
        /// </param>
        /// <param name="partialSelection">
        ///     The items to be partially selected.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="selection"/> is <see langword="null"/>.
        /// </exception>
        public void Select(IEnumerable<TreeItemViewModel> selection, IEnumerable<TreeItemViewModel> partialSelection = null)
        {
            if (selection == null)
            {
                throw new ArgumentNullException(nameof(selection));
            }

            foreach (TreeItemViewModel treeItemViewModel in this.AllItems)
            {
                treeItemViewModel.IsSelected = false;
            }

            foreach (TreeItemViewModel treeItemViewModel in selection)
            {
                treeItemViewModel.IsSelected = true;
            }

            this.DoInternalListBoxSelection();
            this.UpdatePartialSelection(partialSelection);
            this.SelectCommand?.Execute(false);
        }

        /// <summary>
        ///     Clears the selection.
        /// </summary>
        public void Unselect()
        {
            this.Select(Enumerable.Empty<TreeItemViewModel>());
        }

        /// <summary>
        ///     Updates the tree. Must be called when the hierarchy change.
        /// </summary>
        /// <remarks>
        ///     It triggers the Select command.
        /// </remarks>
        public void UpdateTree()
        {
            this.UpdateTreeItems(this.FirstLevel, new List<bool>());

            this.isSelecting = true;
            this.VisibleItems = new FakeObservableList<TreeItemViewModel>(this.GetVisibleTreeItemViewModels(this.FirstLevel));
            this.OnPropertyChanged(nameof(this.VisibleItems));
            this.isSelecting = false;

            this.DoInternalListBoxSelection();
            this.UpdatePartialSelection();
            this.SelectCommand?.Execute(false);
        }

        /// <summary>
        ///     Attempts to bring the provided <paramref name="treeItemViewModel"/> into view.
        /// </summary>
        /// <param name="treeItemViewModel">
        ///     The item that should be brought into view.
        /// </param>
        public void BringIntoView(TreeItemViewModel treeItemViewModel)
        {
            if (treeItemViewModel is null)
            {
                throw new ArgumentNullException(nameof(treeItemViewModel));
            }

            TreeItemViewModel parent = treeItemViewModel.Parent;
            while (parent != null)
            {
                parent.IsExpanded = true;
                parent = parent.Parent;
            }

            // BringIntoView() can't be called on listItemViewModel because of ListBox virtualization.
            this.listBox.ScrollIntoView(treeItemViewModel);
        }

        /// <summary>
        ///     Refreshes the tree. Used when an item is toggled.
        /// </summary>
        internal void RefreshTree()
        {
            this.isSelecting = true;
            this.VisibleItems = new FakeObservableList<TreeItemViewModel>(this.GetVisibleTreeItemViewModels(this.FirstLevel));
            this.OnPropertyChanged(nameof(this.VisibleItems));
            this.isSelecting = false;

            this.DoInternalListBoxSelection();
        }

        internal override void SetView(object view)
        {
            if (this.listBox != null)
            {
                this.listBox.SelectionChanged -= this.OnSelectionChanged;
            }

            this.listBox = view as ListBox;
            if (this.listBox != null)
            {
                this.listBox.SelectionChanged += this.OnSelectionChanged;
            }
        }

        private void DoInternalListBoxSelection()
        {
            if (this.listBox == null)
            {
                return;
            }

            this.isSelecting = true;

            // Depending on the selection mode of the list box.
            switch (this.listBox.SelectionMode)
            {
                // The single selection case.
                case SelectionMode.Single:
                {
                    // Get the one item, that is selected (if there is one.)
                    this.listBox.SelectedItem = this.VisibleItems.SingleOrDefault(x => x.IsSelected);
                    break;
                }

                // The multiple selection case.
                case SelectionMode.Multiple:
                case SelectionMode.Extended:
                {
                    // Invoke the corresponding protected method to select the specified items.
                    SetSelectedItemsMethod.Invoke(this.listBox, new object[] { this.VisibleItems.Where(x => x.IsSelected) });
                    break;
                }
            }

            this.isSelecting = false;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.isSelecting)
            {
                return;
            }

            bool newSelection = (Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == 0;
            if (newSelection)
            {
                foreach (TreeItemViewModel treeItemViewModel in this.AllItems)
                {
                    treeItemViewModel.IsSelected = false;
                }

                foreach (TreeItemViewModel treeItemViewModel in this.listBox.SelectedItems.Cast<TreeItemViewModel>())
                {
                    treeItemViewModel.IsSelected = true;
                }
            }
            else
            {
                foreach (TreeItemViewModel treeItemViewModel in e.AddedItems.Cast<TreeItemViewModel>())
                {
                    treeItemViewModel.IsSelected = true;
                }

                foreach (TreeItemViewModel treeItemViewModel in e.RemovedItems.Cast<TreeItemViewModel>())
                {
                    treeItemViewModel.IsSelected = false;
                }
            }

            this.UpdatePartialSelection();
            this.SelectCommand?.Execute(true);
        }

        private void UpdatePartialSelection(IEnumerable<TreeItemViewModel> partialSelection = null)
        {
            foreach (TreeItemViewModel treeItemViewModel in this.AllItems)
            {
                treeItemViewModel.IsPartiallySelected = false;
            }

            partialSelection = partialSelection ?? Enumerable.Empty<TreeItemViewModel>();

            foreach (TreeItemViewModel treeItemViewModel in this.SelectedItems.Select(x => x.Parent).Union(partialSelection))
            {
                TreeItemViewModel item = treeItemViewModel;
                while (item != null)
                {
                    item.IsPartiallySelected = true;
                    item = item.Parent;
                }
            }
        }

        private IEnumerable<TreeItemViewModel> GetAllTreeItemViewModels(IEnumerable<TreeItemViewModel> itemViewModels)
        {
            if (itemViewModels == null)
            {
                yield break;
            }

            foreach (TreeItemViewModel itemViewModel in itemViewModels)
            {
                yield return itemViewModel;

                foreach (TreeItemViewModel child in this.GetAllTreeItemViewModels(itemViewModel.Children))
                {
                    yield return child;
                }
            }
        }

        private IEnumerable<TreeItemViewModel> GetVisibleTreeItemViewModels(IEnumerable<TreeItemViewModel> itemViewModels)
        {
            if (itemViewModels == null)
            {
                yield break;
            }

            foreach (TreeItemViewModel itemViewModel in itemViewModels)
            {
                yield return itemViewModel;

                if (itemViewModel.IsExpanded)
                {
                    foreach (TreeItemViewModel child in this.GetVisibleTreeItemViewModels(itemViewModel.Children))
                    {
                        yield return child;
                    }
                }
            }
        }

        private void UpdateTreeItems(IList<TreeItemViewModel> itemViewModels, List<bool> hasNextList, TreeItemViewModel parent = null)
        {
            if (itemViewModels == null)
            {
                return;
            }

            for (var index = 0; index < itemViewModels.Count; index++)
            {
                var itemViewModel = itemViewModels[index];

                itemViewModel.HasNextSibling = index < itemViewModels.Count - 1;
                itemViewModel.Parent = parent;
                itemViewModel.TreeElements = new FakeObservableList<bool>(hasNextList);

                hasNextList.Add(itemViewModel.HasNextSibling);

                this.UpdateTreeItems(itemViewModel.Children, hasNextList, itemViewModel);

                hasNextList.RemoveAt(hasNextList.Count - 1);
            }
        }
    }
}