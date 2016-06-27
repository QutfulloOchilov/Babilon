using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babilon.Model
{
    [Table(nameof(Client))]
    public class Client : Person
    {

        #region Dog
        private int dog;
        public int Dog { get { return dog; } set { dog = value; NotifyPropertyChanged(); } }
        #endregion

        #region Balance
        private double balance;
        public double Balance { get { return balance; } set { balance = value; NotifyPropertyChanged(); } }
        #endregion

        #region Docs
        public virtual ICollection<Doc> Docs { get; set; }
        #endregion

        #region Operations
        public virtual ICollection<Operation> Operations { get; set; }
        #endregion

        #region Prices
        public virtual ICollection<Price> Prices { get; set; }
        #endregion

    }
}
