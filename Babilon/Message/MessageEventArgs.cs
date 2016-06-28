using System;

namespace Babilon.Message
{
    public  class MessageManagerEventArgs : EventArgs
    {
        public Message Message { get; set; }

        public MessageManagerEventArgs(Message message)
        {
            this.Message = message;
        }
    }

    public static class MessageManager
    {

        #region MessageManagerEvent Event
        public delegate void MessageManagerEventHendler(MessageManagerEventArgs e);

        public static event MessageManagerEventHendler ShowMessage;
        public static void OnMessageManagerEvent(Message message)
        {
            ShowMessage?.Invoke(new MessageManagerEventArgs(message));
        }

        #endregion
    }
}
