using System;

namespace Timetracker.Core.Domain.Interface
{

    public interface IEntity
    {
        object Id { get; }
        DateTime CreatedOn { get; set; }
        DateTime? UpdatedOn { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }


    }

    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }
}