using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babilon.Model
{
    [Table(nameof(Company))]
    public class Company : EntityBase
    {
        #region Name
        public string Name { get; set; }
        #endregion

        #region Tariffs
        public virtual ICollection<Tariff> Tariffs { get; set; }
        #endregion
    }
}
