using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WorldEvents.Entities
{
    public class ApplicationUser : IdentityUser//<Guid>
    {
        //[ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; } = new UserProfile();// Default profile will be created

        //TODO: !!!
        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        ///Site role (not project role) 
        /// 1 - admin
        /// 2 - content manager
        /// 3 - simple registered user (can be subscribed to cateories, create new project, add participans, contact)
        public virtual ApplicationRole Role { get; set; }

        /// <summary>
        /// Categories to which user is subscribed
        /// </summary>
        //[ForeignKey("UserId")]
        public virtual ICollection<CategorySubscription> Subscriptions { get; set; } = new List<CategorySubscription>();
    }
}
