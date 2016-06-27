using Babilon.Model;
using Babilon.Pages;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Babilon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            viewModel.CurrentContent.Content = new MainPage();
            viewModel.CurrentContent.DataContext = viewModel;
            this.DataContext = viewModel;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //  ShowLoginAsync();
        }

        private async void ShowLoginAsync()
        {
            var result = await this.ShowLoginAsync("Authentication", "Enter your password", new LoginDialogSettings
            {
                ColorScheme = this.MetroDialogOptions.ColorScheme,
                DefaultButtonFocus = MessageDialogResult.Affirmative
            });
            if (result == null)
            {
                ShowLoginAsync();
            }
            else if (!await viewModel.CheckLoginAddPassword(result.Username, result.Password))
            {
                ShowLoginAsync();
            }
        }

        private void uiLock_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowLoginAsync();
        }
    }
}
