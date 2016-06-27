using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babilon.Model
{
    [Table(nameof(Simcard))]
    public class Simcard : EntityBase
    {

        public Guid PriceId { get; set; }

        [ForeignKey(nameof(PriceId))]
        public virtual Price Price { get; set; }


        private string number;
        public string Number { get { return number; } set { number = value; NotifyPropertyChanged(); } }

        private bool hastClient;
        public bool HasClient { get { return hastClient; } set { hastClient = value; NotifyPropertyChanged(); } }


        private bool hasDoc;
        public bool HasDoc { get { return hasDoc; } set { hasDoc = value; NotifyPropertyChanged(); } }

        public Guid TariffId { get; set; }

        [ForeignKey(nameof(TariffId))]
        public Tariff Tariff { get; set; }



    }
}
