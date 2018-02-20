using System.Threading.Tasks;

namespace WorldEvents.Entities
{
    public interface IEventRegistrationPolicy
    {
        Task CheckRegistrationAttemptAsync(Event @event, ApplicationUser user);
    }
}