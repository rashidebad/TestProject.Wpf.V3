// <copyright file="BooleanToHiddenVisibilityConverter.cs" company="ABC Inc.">
// Copyright (c) ABC Inc. All rights reserved.
// </copyright>

namespace TestProject.Wpf.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    ///     Represents the converter that converts a boolean to visibility.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToHiddenVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as Visibility? == Visibility.Visible;
        }
    }
}