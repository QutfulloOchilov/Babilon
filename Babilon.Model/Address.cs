using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Babilon.Model
{
    [Table(nameof(Address))]
    public class Address : EntityBase
    {
        private string name;
        public string Name { get { return name; } set { name = value; NotifyPropertyChanged(); } }


        public virtual ICollection<Client> Clients { get; set; }
    }
}
