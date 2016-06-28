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

namespace Babilon
{
    public class ViewModel : INotifyPropertyChanged
    {
        private User currentUser;
        private ObservableCollection<Menu> menus;

        public ViewModel()
        {
            CreateDefaultUser();
            menus = new ObservableCollection<Menu>();
            CreateDefaultMenu();
            currentContent = new ContentControl();
            SampleSelectedQuery();
        }

        private void SampleSelectedQuery()
        {
            ///changed
            List<User> users;
            using (var context = new EntityContext())
            {
                users = context.Users.Where(u => u.FirstName == "Qutfullo").ToList();
            }
            var uuuu = users;
        }

        #region CurrentUser

        public User CurrentUser { get { return currentUser; } set { currentUser = value; NotifyPropertyChanged(); } }

        public async void CreateDefaultUser()
        {
            using (var context = new EntityContext())
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
                    Client dilovarClient = new Client
                    {
                        FirstName = "Dilovar",
                        LastName = "Otaev",
                        Telephon = "+992987000874"
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
                    dilovarClient.Address = newAddress;
                    context.Clients.Add(dilovarClient);
                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();
                }
            }
        }

        public Task<bool> CheckLoginAddPassword(string login, string password)
        {
            var taskCheck = Task.Factory.StartNew(() =>
              {
                  using (var context = new EntityContext())
                  {
                      var resultCheck = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                      return resultCheck != null;
                  }
              });
            taskCheck.Wait(100000);
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

            #region Control
            Menu controlCustomerMenu = new Menu();
            controlCustomerMenu.Title = "Кор бо мизоҷ";
            //< materialDesign:PackIcon Kind = "AccountMultiple" />
            controlCustomerMenu.Icon = "AccountMultiple";

            Menu clientMenu = new Menu();
            clientMenu.Title = "Мизоҷон";
            clientMenu.Icon = "FormatListBulleted";
            controlCustomerMenu.Childs.Add(clientMenu);
            //< materialDesign:PackIcon Kind = "FormatListBulleted" />
            #endregion

            //Adding menu
            Menus.Add(controlSimMenu);
            Menus.Add(controlCustomerMenu);
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

        private Menu selectedMenu;

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
