
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldEvents.Entities
{
    public class ApplicationUser : IdentityUser//<Guid>
    {
        public virtual UserProfile UserProfile { get; set; }

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

    //class IdentityUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUser, IUser<string>
    //{
    //    IdentityUser()
    //    {
    //        this.Id = Guid.NewGuid().ToString();
    //    }

    //    IdentityUser(string userName) : this()
    //    {
    //        this.UserName = userName;
    //    }
    //}
}
