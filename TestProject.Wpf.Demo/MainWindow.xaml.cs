// <copyright file="MainWindow.xaml.cs" company="ABC Inc.">
// Copyright (c) ABC Inc. All rights reserved.
// </copyright>

namespace TestProject.Wpf.Demo
{
    using System.Collections.Generic;
    using System.Windows;
    using TestProject.Wpf.ViewModels;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {

            this.SimpleTreeViewModel.FirstLevel = this.CreateTree(this.SimpleTreeViewModel, "Tree item ", 6, 8);
            this.SimpleTreeViewModel.UpdateTree();

            this.DataContext = this;
            this.InitializeComponent();
        }

        /// <summary>
        ///     Gets the ViewModel of this View.
        /// </summary>
        public TreeViewModel SimpleTreeViewModel { get; } = new TreeViewModel();

        private List<TreeItemViewModel> CreateTree(TreeViewModel treeViewModel, string prefix, int width, int depth)
        {
            if (depth <= 0)
            {
                return null;
            }

            var list = new List<TreeItemViewModel>();

            for (int i = 1; i <= width; i++)
            {
                var item = new DemoTreeItemViewModel(treeViewModel);
                item.Caption = prefix + i;
                item.Children = this.CreateTree(treeViewModel, prefix + i + ".", width - i, depth - 1);
                list.Add(item);
            }

            return list;
        }
    }
}
