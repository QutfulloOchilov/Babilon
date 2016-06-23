using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Babilon.Model
{
    [Table(nameof(Company))]
    public class Company: EntityBase
    {
        [Column]
        public string Name { get; set; }

        public virtual ICollection<Tariff> Tariffs { get; set; }
    }
}
