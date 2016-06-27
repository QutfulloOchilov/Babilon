namespace Babilon.Model
{
    public class User : Person
    {
        private string login;
        public string Login { get { return login; } set { login = value; NotifyPropertyChanged(); } }


        private string password;
        public string Password { get { return password; } set { password = value; NotifyPropertyChanged(); } }


    }
}
