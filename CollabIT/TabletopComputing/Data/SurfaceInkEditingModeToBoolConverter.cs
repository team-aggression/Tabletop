using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Microsoft.Surface.Presentation.Controls;

namespace TabletopComputing.Data
{
    public class SurfaceInkEditingModeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is SurfaceInkEditingMode)
            {
                SurfaceInkEditingMode editingMode = (SurfaceInkEditingMode)value;
                return editingMode == SurfaceInkEditingMode.EraseByPoint;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                bool isErasing = (bool)value;
                return isErasing ? SurfaceInkEditingMode.EraseByPoint : SurfaceInkEditingMode.Ink;
            }

            return SurfaceInkEditingMode.None;
        }
    }
}
