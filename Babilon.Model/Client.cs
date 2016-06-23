using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Babilon.Model
{
    [Table(nameof(Client))]
    public class Client : EntityBase
    {

        private string firstName;
        public string FirstName { get { return firstName; } set { firstName = value; NotifyPropertyChanged(); } }


        private string lastName;
        public string LastName { get { return lastName; } set { lastName = value; NotifyPropertyChanged(); } }


        private string telephon;
        public string Telephon { get { return telephon; } set { telephon = value; NotifyPropertyChanged(); } }


        private int dog;
        public int Dog { get { return dog; } set { dog = value; NotifyPropertyChanged(); } }


        private double balance;
        public double Balance { get { return balance; } set { balance = value; NotifyPropertyChanged(); } }


        public Guid AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }


    }
}
