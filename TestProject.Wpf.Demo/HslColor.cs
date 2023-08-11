// <copyright file="HslColor.cs" company="ABC Inc.">
// Copyright (c) ABC Inc. All rights reserved.
// </copyright>

namespace TestProject.Wpf
{
    using System;
    using System.Windows.Markup;
    using System.Windows.Media;

    /// <summary>
    ///     This class is used to describe colors in HSLA (Hue, Saturation, Lightness, Alpha) form.
    ///     It also handles conversions to standard RGB values.
    /// </summary>
    [MarkupExtensionReturnType(typeof(Color))]
    public class HslColor : MarkupExtension
    {
        /// <summary>
        ///     Gets or sets hue (0.0 - 360.0).
        /// </summary>
        public double H { get; set; }

        /// <summary>
        ///     Gets or sets saturation (0.0 - 100.0).
        /// </summary>
        public double S { get; set; }

        /// <summary>
        ///    Gets or sets lightness (0.0 - 100.0).
        /// </summary>
        public double L { get; set; }

        /// <summary>
        ///     Gets or sets alpha (0.0 - 100.0).
        /// </summary>
        public double A { get; set; }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // Convert HSL to RGB.
            double h = Math.Min(Math.Max(0.0, this.H * (1.0 / 360.0)), 1.0);
            double s = Math.Min(Math.Max(0.0, this.S * (1.0 / 100.0)), 1.0);
            double l = Math.Min(Math.Max(0.0, this.L * (1.0 / 100.0)), 1.0);
            float a = Math.Min(Math.Max(0.0f, (float)(this.A * (1.0 / 100.0))), 1.0f);

            double r, g, b;

            if (s < double.Epsilon)
            {
                r = l;
                g = l;
                b = l;
            }
            else
            {
                double tmp2;

                if (l < 0.5)
                {
                    tmp2 = l * (1.0 + s);
                }
                else
                {
                    tmp2 = (l + s) - (l * s);
                }

                double tmp1 = (2.0 * l) - tmp2;

                r = h + (1.0 / 3.0);
                if (r > 1.0)
                {
                    r--;
                }

                g = h;

                b = h - (1.0 / 3.0);
                if (b < 0.0)
                {
                    b++;
                }

                r = GetRgb(r, tmp1, tmp2);
                g = GetRgb(g, tmp1, tmp2);
                b = GetRgb(b, tmp1, tmp2);
            }

            // Convert RGB to scRGB to get higher precision.
            float scr = GetScRgb(r);
            float scg = GetScRgb(g);
            float scb = GetScRgb(b);

            // Return the final color.
            return Color.FromScRgb(a, scr, scg, scb);
        }

        private static double GetRgb(double v, double tmp1, double tmp2)
        {
            if (v < 1.0 / 6.0)
            {
                return tmp1 + ((tmp2 - tmp1) * 6.0f * v);
            }

            if (v < 0.5)
            {
                return tmp2;
            }

            if (v < 2.0 / 3.0)
            {
                return tmp1 + ((tmp2 - tmp1) * ((2.0 / 3.0) - v) * 6.0);
            }

            return tmp1;
        }

        private static float GetScRgb(double rgb)
        {
            if (rgb < double.Epsilon)
            {
                return 0.0f;
            }

            if (rgb <= 0.04045)
            {
                return (float)(rgb * (1.0 / 12.92));
            }

            if (rgb < 1.0)
            {
                return (float)Math.Pow((rgb + 0.055) * (1.0 / 1.055), 2.4);
            }

            return 1.0f;
        }
    }
}
