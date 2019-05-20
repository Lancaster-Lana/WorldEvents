using System.Threading.Tasks;
using WorldEvents.Entities;

namespace WorldEvents.Core.Events
{
    public interface IEventRegistrationPolicy
    {
        Task CheckRegistrationAttemptAsync(Event @event, ApplicationUser user);
    }
}