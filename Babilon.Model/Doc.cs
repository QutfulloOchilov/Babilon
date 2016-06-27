using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babilon.Model
{
    [Table(nameof(Doc))]
    public class Doc : EntityBase
    {

        private Guid clientId;
        public Guid ClientId { get { return clientId; } set { clientId = value; NotifyPropertyChanged(); } }


        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }
    }
}
