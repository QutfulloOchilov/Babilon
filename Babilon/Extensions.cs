using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Babilon
{
    public static class Extensions
    {

        /// <summary>
        /// Finds the background property of a Control or Panel (it's not the same property) and assigns it.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="backgroundBrush">Brush to assign the background property to</param>
        public static void SetBackground(this FrameworkElement element, Brush backgroundBrush)
        {
            var control = element as Control;
            if (control != null)
            {
                control.Background = backgroundBrush;
                return;
            }

            var panel = element as Panel;
            if (panel != null)
            {
                panel.Background = backgroundBrush;
                return;
            }
        }

        public static IEnumerable<DependencyObject> GetVisualAncestry(this DependencyObject leaf)
        {
            while (leaf != null)
            {
                yield return leaf;
                leaf = VisualTreeHelper.GetParent(leaf);
            }
        }

        /// <summary>
        /// Converter hex color string to SolidColorBrush
        /// </summary>
        /// <param name="hexColor"></param>
        /// <returns></returns>
        public static SolidColorBrush ToBrush(this string hexColor)
        {
            return new BrushConverter().ConvertFrom(hexColor) as SolidColorBrush;
        }

        #region Find elements in the visual tree

        /// <summary>
        /// Get all children in visual tree of type T and satisfying the filter delegate (optional)
        /// </summary>
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj, bool includeChildrenOfMatching = true, Func<T, bool> filter = null) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    bool checkChildren = true;

                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T && (filter == null || filter((T)child)))
                    {
                        yield return (T)child;
                        checkChildren = includeChildrenOfMatching;
                    }

                    if (checkChildren)
                    {
                        foreach (T childOfChild in FindVisualChildren<T>(child))
                            yield return childOfChild;
                    }
                }
            }
        }

        /// <summary>
        /// Method to get child control of specified type
        /// </summary>
        /// <typeparam name="T">Type of child control queried</typeparam>
        /// <param name="parent">Reference of parent control in which child control resides</param>
        /// <param name="filter">Filter items by</param>
        /// <returns>Returns reference of child control of specified type (T) if found, otherwise it will return null.</returns>
        public static T FindVisualChild<T>(this DependencyObject parent, Func<T, bool> filter = null) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T && (filter == null || filter((T)child)))
                {
                    return (T)(object)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child, filter);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="element">A direct or indirect child of the queried item.</param>
        /// <param name="name">Element Name property that the found parent must match</param>
        /// <param name="filter">Filter items by</param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, a null reference is being returned.</returns>
        public static T FindVisualParent<T>(this DependencyObject element, string name = null, Func<T, bool> filter = null)
          where T : DependencyObject
        {
            return FindVisualParent<T>(element, name, filter, false);
        }

        /// <summary>
        /// Finds the last parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="element">A direct or indirect child of the queried item.</param>
        /// <param name="name">Element Name property that the found parent must match</param>
        /// <param name="filter">Filter items by</param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, a null reference is being returned.</returns>
        public static T FindLastVisualParent<T>(this DependencyObject element, string name = null, Func<T, bool> filter = null)
          where T : DependencyObject
        {
            return FindVisualParent<T>(element, name, filter, true);
        }

        static DependencyObject GetParent(this DependencyObject element)
        {
            DependencyObject parent = null;
            try
            {
                parent = VisualTreeHelper.GetParent(element);
            }
            catch (InvalidOperationException)
            {
                parent = null;
            }
            if (parent == null)
            {
                FrameworkElement frameworkElement = element as FrameworkElement;
                if (frameworkElement != null)
                {
                    parent = frameworkElement.Parent;
                }
                FrameworkContentElement frameworkContentElement = element as FrameworkContentElement;
                if (frameworkContentElement != null)
                {
                    parent = frameworkContentElement.Parent;
                }
            }
            return parent;
        }

        static T FindVisualParent<T>(DependencyObject element, string name, Func<T, bool> filter, bool findLast)
          where T : DependencyObject
        {
            // get parent item
            DependencyObject parentObject = element.GetParent();

            // we’ve reached the end of the tree
            if (parentObject == null) return null;

            // check if the parent matches the type we’re looking for
            T parent = parentObject as T;
            if (parent != null && (name == null || NameMatches(parent as FrameworkElement, name)) && (filter == null || filter((T)parent)))
            {
                // use recursion to proceed with next level
                if (findLast)
                {
                    return FindVisualParent<T>(parentObject, name, filter, findLast) ?? parent;
                }
                else return parent;
            }
            else
            {
                // use recursion to proceed with next level
                return FindVisualParent<T>(parentObject, name, filter, findLast);
            }
        }

        static bool NameMatches(FrameworkElement element, string name)
        {
            if (element == null || name == null)
                return true;

            return element.Name == name;
        }

        #endregion

        /// <summary>
        /// Mirror the object in the UI with render transform
        /// </summary>
        /// <param name="element"></param>
        /// <param name="orientation">Mirror vertically or horizontally</param>
        public static void Mirror(this UIElement element, Orientation orientation)
        {
            element.RenderTransformOrigin = new Point(0.5, 0.5);

            if (orientation == Orientation.Vertical)
                element.RenderTransform = new ScaleTransform() { ScaleY = -1 };
            else
                element.RenderTransform = new ScaleTransform() { ScaleX = -1 };
        }

        #region Progress bars

        /// <summary>
        /// Start the progress bar (set IsIndeterminate to true)
        /// </summary>
        public static void Start(this ProgressBar progressBar)
        {
            progressBar.IsIndeterminate = true;
        }

        /// <summary>
        /// Stop the progress bar (set IsIndeterminate to false)
        /// </summary>
        public static void Stop(this ProgressBar progressBar)
        {
            progressBar.IsIndeterminate = false;
        }

        #endregion

        /// <summary>
        /// Returns font's width from the given font size and family
        /// </summary>
        /// <param name="length">The number of characters to measure</param>
        /// <param name="character">Character to measure with</param>
        /// <returns>Size object with height and width of the inputted text</returns>
        public static Size GetDisplaySize(int length, char character = '0')
        {
            string stringToMeasure = "";
            for (int i = 0; i < length; i++)
            {
                stringToMeasure += character;
            }
            return stringToMeasure.GetDisplaySize();
        }

        /// <summary>
        /// Returns font's width from the given font size and family
        /// </summary>
        /// <param name="text">The text whose size we should judge</param>
        /// <returns>Size object with height and width of the inputted text</returns>
        public static Size GetDisplaySize(this string text)
        {
            Typeface myTypeface = new Typeface("Helvetica");
            var ft = new FormattedText(text, CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight, myTypeface, 16, Brushes.Red);

            ft.SetFontSize((double)Application.Current.TryFindResource("EditControlFontSize"));
            ft.SetFontFamily((FontFamily)Application.Current.TryFindResource("EditControlFontFamily"));

            return new Size(ft.Width, ft.Height);
        }

        #region Window taskbar flashing


        #endregion


    }
}