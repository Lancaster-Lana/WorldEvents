
using Microsoft.AspNetCore.Identity;
using System;

namespace WorldEvents.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser//<Guid>
    {

    }

    //class IdentityUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole,
    //IdentityUserClaim>, IUser, IUser<string>
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
