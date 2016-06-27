using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Babilon.ControlMenu
{
    public class Menu : INotifyPropertyChanged
    {
        private string icon;
        private Color color;
        private string title;
        private ContentControl pageForSelectedMenu;
        private ObservableCollection<Menu> childs;

        public Menu()
        {
            childs = new ObservableCollection<Menu>();
            pageForSelectedMenu = new ContentControl();
        }

        public string Icon { get { return icon; } set { icon = value; NotifyPropertyChanged(); } }

        public Color Color { get { return color; } set { color = value; NotifyPropertyChanged(); } }

        public string Title
        {
            get { return title; }
            set { title = value; NotifyPropertyChanged(); }
        }



        public ContentControl PageForSlectedMenu
        {
            get { return pageForSelectedMenu; }
            set { pageForSelectedMenu = value; }
        }


        public ObservableCollection<Menu> Childs { get { return childs; } set { childs = value; NotifyPropertyChanged(); } }

        #region Notify

        /// <summary>
        /// Property Changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fire the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property that changed (defaults from CallerMemberName)</param>
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
