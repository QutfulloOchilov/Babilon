using Menu = Babilon.ControlMenu.Menu;
using Babilon.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Babilon.Pages;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data.Entity.Core;
using Babilon.Message;

namespace Babilon
{
    public class ViewModel : INotifyPropertyChanged
    {
        private User currentUser;
        private ObservableCollection<Menu> menus;
        private Menu selectedMenu;

        public ViewModel()
        {
            try
            {
                CreateDefaultUser();
                menus = new ObservableCollection<Menu>();
                CreateDefaultMenu();
                currentContent = new ContentControl();
            }
            catch (ProviderIncompatibleException e)
            {
                CreateExcaption(e);
            }
        }

        private void CreateExcaption(Exception e)
        {
            var innerException = e.InnerException;
            if (innerException.InnerException != null && innerException.InnerException is MySqlException && (innerException.InnerException as MySqlException).Number == 1042)
            {
                var connectionException = innerException?.InnerException;
                var connectionMessage = new Message.Message
                {
                    Title = "Базаи маълумот дастнорас аст!",
                    Detail = "Барнома ба базаи маълумот пайваст шуда натавониста истодааст.",
                    Solution = "Шумо бояд ба базаи маълумот дастраси дошта бошед!"
                };
                connectionMessage.Choices.Add(new MessageChoice("OK") { Message = connectionMessage });
                MessageManager.OnMessageManagerEvent(connectionMessage);
            }

        }

        #region CurrentUser

        public User CurrentUser { get { return currentUser; } set { currentUser = value; NotifyPropertyChanged(); } }

        public async void CreateDefaultUser()
        {
            using (var context = new EntityContext())
            {
                try
                {
                    User newUser = context.Users.FirstOrDefault(u => u.Login == "qutfullo");
                    if (newUser == null)
                    {
                        newUser = new User()
                        {
                            FirstName = "Qutfullo",
                            LastName = "OChilov",
                            Email = "kutfullo@mail.ru",
                            Login = "qutfullo",
                            Password = "123",
                            Telephon = "+992987807042"
                        };

                        Address newAddress = context.Addresses.FirstOrDefault(a => a.Name == "m.Yasi");
                        if (newAddress == null)
                        {
                            newAddress = new Address
                            {
                                Name = "m.Yasi"
                            };
                            context.Addresses.Add(newAddress);
                        }
                        newUser.Address = newAddress;
                        context.Users.Add(newUser);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    CreateExcaption(e);

                }
            }
        }

        public Task<bool> CheckLoginAddPassword(string login, string password)
        {
            var taskCheck = Task.Factory.StartNew(() =>
              {
                  using (var context = new EntityContext())
                  {
                      bool result = false;
                      var resultCheck = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                      if (resultCheck != null)
                      {
                          CurrentUser = resultCheck;
                          result = true;
                      }
                      return result;
                  }
              });
            return taskCheck;
        }


        #endregion

        #region Menu

        public ObservableCollection<Menu> Menus
        {
            get { return menus; }
            set { menus = value; }
        }

        private void CreateDefaultMenu()
        {

            #region Sim
            Menu controlSimMenu = new Menu();
            controlSimMenu.Icon = "ArrangeSendToBack";
            controlSimMenu.Title = "Идоракунии симкорт";

            Menu importSim = new Menu();
            importSim.Icon = "Import";
            importSim.Title = "Импорт";
            importSim.PageForSlectedMenu.Content = new ImportPage();

            Menu exportSim = new Menu();
            exportSim.Icon = "Export";
            exportSim.Title = "Экспорт";

            //Adding sub menu Sim
            controlSimMenu.Childs.Add(importSim);
            controlSimMenu.Childs.Add(exportSim);
            #endregion

            #region ControlCustomer
            Menu controlCustomerMenu = new Menu();
            controlCustomerMenu.Title = "Кор бо мизоҷ";
            controlCustomerMenu.Icon = "AccountMultiple";

            Menu clientMenu = new Menu();
            clientMenu.Title = "Мизоҷон";
            clientMenu.Icon = "FormatListBulleted";

            Menu addNewClientMenu = new Menu();
            addNewClientMenu.Title = "Дохил кардани мизоҷи нав";
            addNewClientMenu.Icon = "AccountMultiplePlus";
            addNewClientMenu.PageForSlectedMenu = new AddCustomerPage();

            //Adding sub menu to controlCustomer
            controlCustomerMenu.Childs.Add(clientMenu);
            controlCustomerMenu.Childs.Add(addNewClientMenu);

            #endregion

            //Adding menu
            Menus.Add(controlSimMenu);
            Menus.Add(controlCustomerMenu);
        }

        public Menu SelectedMenu
        {
            get { return selectedMenu; }
            set
            {
                if (selectedMenu != value)
                {
                    selectedMenu = value;
                    CurrentContent.Content = selectedMenu.PageForSlectedMenu;
                }
            }
        }

        #endregion

        private ContentControl currentContent;

        public ContentControl CurrentContent
        {
            get { return currentContent; }
            set
            {
                if (value != null && currentContent != value)
                {
                    currentContent = value;
                }
            }
        }

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
