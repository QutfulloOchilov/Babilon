using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babilon.Model
{
    [Table(nameof(Operation))]
    public class Operation : EntityBase
    {

        private int count;
        public int Count { get { return count; } set { count = value; NotifyPropertyChanged(); } }


        private double totalPrice;
        public double TotalPrice { get { return totalPrice; } set { totalPrice = value; NotifyPropertyChanged(); } }


        private DateTime date;
        public DateTime Date { get { return date; } set { date = value; NotifyPropertyChanged(); } }


        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }
    }
}
