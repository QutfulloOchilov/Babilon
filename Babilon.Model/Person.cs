using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babilon.Model
{
    public abstract class Person : EntityBase
    {
        private string firstName;
        public string FirstName { get { return firstName; } set { firstName = value; NotifyPropertyChanged(); } }


        private string lastName;
        public string LastName { get { return lastName; } set { lastName = value; NotifyPropertyChanged(); } }


        private string telephon;
        public string Telephon { get { return telephon; } set { telephon = value; NotifyPropertyChanged(); } }


        private string email;
        public string Email { get { return email; } set { email = value; NotifyPropertyChanged(); } }


        private Guid addressId;
        public Guid AddressId
        {
            get { return addressId; }
            set
            {
                addressId = value;
                NotifyPropertyChanged();
            }
        }

        private Address address;
        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get { return address; } set { address = value; NotifyPropertyChanged(); } }

    }
}
