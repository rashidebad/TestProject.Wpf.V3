// <copyright file="DemoTreeItemViewModel.cs" company="ABC Inc.">
// Copyright (c) ABC Inc. All rights reserved.
// </copyright>

namespace TestProject.Wpf.Demo
{
    using TestProject.Wpf.ViewModels;

    /// <summary>
    ///     Represents the ViewModel for an item in the tree demo.
    /// </summary>
    public class DemoTreeItemViewModel : TreeItemViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DemoTreeItemViewModel"/> class.
        /// </summary>
        /// <param name="treeViewModel">
        ///     The parent tree ViewModel.
        /// </param>
        public DemoTreeItemViewModel(TreeViewModel treeViewModel)
            : base(treeViewModel)
        {
        }

        /// <summary>
        ///     Gets or sets the caption.
        /// </summary>
        public string Caption { get; set; }
    }
}
