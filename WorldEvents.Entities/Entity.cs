using System;

namespace WorldEvents.Entities
{
    /// <summary>
    /// TODO: to be removed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Entity<T> 
    {
        public virtual T Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        //public virtual bool IsTransient()
        //{
        //    return this.Id == default(T);
        //}
    }
}