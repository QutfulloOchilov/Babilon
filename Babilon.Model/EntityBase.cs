using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babilon.Model
{
    public abstract class EntityBase : IEntity, INotifyPropertyChanged
    {
        private Guid id;
        [Key]
        [Column]
        public Guid Id
        {
            get
            {
                if (id.ToString().StartsWith("0000"))
                {
                    id = Guid.NewGuid();
                }
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        #region Notify

        /// <summary>
        /// Property Changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fire the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property that changed (defaults from CallerMemberName)</param>
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
