using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babilon.Model
{
    [Table(nameof(Tariff))]
    public class Tariff : EntityBase
    {
        public string Name { get; set; }
        public Guid CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        public virtual ICollection<Simcard> Simcards { get; set; }


    }
}
