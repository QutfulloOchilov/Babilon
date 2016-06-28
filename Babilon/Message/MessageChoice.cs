using System;

namespace Babilon.Message
{
    public class MessageChoice
    {
        private string title;

        public MessageChoice(string title)
        {
            this.title = title;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        public Action<MessageChoice> Selected;

        public Message Message { get; set; }
    }
}
