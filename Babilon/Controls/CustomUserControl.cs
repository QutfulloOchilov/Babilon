using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Babilon.Controls
{
    public class CustomUserControl : UserControl
    {
        public virtual string Title { get; }

        protected void ChangeTitle(string title)
        {
            var mainWindow = this.FindVisualParent<MetroWindow>("uiMainWindow");
            mainWindow.Title=title
        }

    }
}
