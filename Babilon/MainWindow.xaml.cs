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
using Babilon.Message;
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

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageManager.ShowMessage += MessageManager_ShowMessage;
                viewModel = new ViewModel();
                viewModel.CurrentContent.Content = new MainPage();
                viewModel.CurrentContent.DataContext = viewModel;
                this.DataContext = viewModel;
            }
            catch (Exception ex)
            {
                var iii = ex;
                
            }
        }

        private async void MessageManager_ShowMessage(MessageManagerEventArgs e)
        {
            var message = e.Message;
            var messageSetting = new MetroDialogSettings();
            var messageDialogStyle = MessageDialogStyle.Affirmative;
            switch (message.Choices.Count)
            {
                case 1:
                    messageSetting.AffirmativeButtonText = message.Choices.Single().Title;
                    break;
                case 2:
                    messageSetting.AffirmativeButtonText = message.Choices[0].Title;
                    messageSetting.NegativeButtonText = message.Choices[1].Title;
                    messageDialogStyle = MessageDialogStyle.AffirmativeAndNegative;
                    break;
                case 3:
                    messageSetting.AffirmativeButtonText = message.Choices[0].Title;
                    messageSetting.NegativeButtonText = message.Choices[1].Title;
                    messageSetting.FirstAuxiliaryButtonText = message.Choices[2].Title;
                    messageDialogStyle = MessageDialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary;
                    break;
                default:
                    break;
            }

            await this.ShowMessageAsync(message.Title, message.Detail,
                messageDialogStyle, messageSetting);
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
