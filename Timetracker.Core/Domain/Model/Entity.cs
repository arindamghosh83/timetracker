using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Timetracker.Core.Domain.Interface;

namespace Timetracker.Core.Domain.Model
{
	public abstract class Entity<T>:IEntity<T>
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public  T Id { get; set; }

	    private DateTime? createdOn;
	    [DataType(DataType.DateTime)]
        public DateTime CreatedOn {
            get
            {
                return createdOn ?? DateTime.UtcNow;
            }
            set
            {
                createdOn = value;
            }
        }
	    private DateTime? updatedOn;
        public DateTime? UpdatedOn {
            get
            {
                return updatedOn ?? DateTime.UtcNow;
            }
            set
            {
                updatedOn = value;
            }
        }
	    public string CreatedBy { get; set; }
	    public string UpdatedBy { get; set; }


	    object IEntity.Id => this.Id;
        

        public override bool Equals(object obj)
		{
			var compareTo = obj as Entity<T>;

			if (ReferenceEquals(this, compareTo)) return true;
			if (ReferenceEquals(null, compareTo)) return false;

			return Id.Equals(compareTo.Id);
		}

		public static bool operator ==(Entity<T> a, Entity<T> b)
		{
			if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
				return true;

			if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(Entity<T> a, Entity<T> b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return (GetType().GetHashCode() * 907) + Id.GetHashCode();
		}

		public override string ToString()
		{
			return GetType().Name + " [Id=" + Id + "]";
		}
	}


	public class Entity
	{

	}
}
