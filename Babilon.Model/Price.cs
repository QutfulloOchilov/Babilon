using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babilon.Model
{
    [Table(nameof(Model.Price))]
    public class Price : EntityBase
    {
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }

        public Guid TariffId { get; set; }

        [ForeignKey(nameof(TariffId))]
        public virtual Tariff Tariff { get; set; }


        private double value;
        public double Value { get { return value; } set { this.value = value; NotifyPropertyChanged(); } }

    }
}
