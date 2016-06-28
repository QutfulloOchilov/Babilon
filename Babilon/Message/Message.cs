using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Babilon.Message
{
    public class Message : INotifyPropertyChanged
    {

        private string title;
        private MessageLevel messageLevel;
        private string solution;
        private string detail;
        private ObservableCollection<MessageChoice> choices;

        public Message()
        {
            choices = new ObservableCollection<MessageChoice>();
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<MessageChoice> Choices { get { return choices; } set { choices = value; } }

        /// <summary>
        /// 
        /// </summary>
        public MessageLevel MessageLevel { get { return messageLevel; } set { messageLevel = value; NotifyPropertyChanged(); } }

        /// <summary>
        /// 
        /// </summary>

        public string Title { get { return title; } set { title = value; NotifyPropertyChanged(); } }

        /// <summary>
        /// 
        /// </summary>
        public string Solution { get { return solution; } set { solution = value; NotifyPropertyChanged(); } }

        /// <summary>
        /// 
        /// </summary>
        public string Detail { get { return detail; } set { detail = value; NotifyPropertyChanged(); } }

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

    public enum MessageLevel
    {
        Error,
        Warning,
        Information
    }
}
