using Abp.Events.Bus.Entities;

namespace WorldEvents.Entities
{
    public class EventCancelledEvent : EntityEventData<Event>
    {
        public EventCancelledEvent(Event entity) : base(entity)
        {
        }
    }
}