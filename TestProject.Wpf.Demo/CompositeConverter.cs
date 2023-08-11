// <copyright file="CompositeConverter.cs" company="ABC Inc.">
// Copyright (c) ABC Inc. All rights reserved.
// </copyright>

namespace TestProject.Wpf.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Data;

    /// <summary>
    ///     Represents the converter that chain multiple converters.
    /// </summary>
#pragma warning disable CA1710 // Identifiers should have correct suffix
    public class CompositeConverter : List<IValueConverter>, IValueConverter
#pragma warning restore CA1710 // Identifiers should have correct suffix
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable<IValueConverter> reversed = this.AsEnumerable().Reverse();
            return reversed.Aggregate(value, (current, converter) => converter.ConvertBack(current, targetType, parameter, culture));
        }
    }
}
