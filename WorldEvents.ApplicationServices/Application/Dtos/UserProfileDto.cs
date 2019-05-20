using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorldEvents.Entities;

namespace WorldEvents.Application.Dto
{
    /// <summary>
    /// UserProfile belong for some ApplicationUser.
    /// </summary>
    [AutoMap(typeof(UserProfile))]
    public class UserProfileDto : FullAuditedEntityDto<Guid> //AbpUserDto<UserProfile>  
    {
        /// <summary>
        /// Parent user entity
        /// </summary>
        public ApplicationUserDto User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public string FullName
        //{
        //    get
        //    {
        //        return LastName + ' ' + FirstName;
        //    }
        //}

        public string Email { get; set; }

        /// <summary>
        /// 0 -male, 1-female
        /// </summary>
        public bool? Gender { get; set; }

        //[Range(new DateTime(1900, 1, 1), DateTime.Now]
        public DateTime? Birthday { get; set; }

        //[Range(1, 100)]
        //public uint Age { get; set; }

        //public virtual Address AddressContact { get; set; }

        //public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

        /// <summary>
        /// Several social contacts, like : linkedin, facebook, yachoo,  
        /// </summary>
        //public virtual ICollection<Contact> UserContacts { get; set; } = new List<Contact>();

        /// <summary>
        /// 
        /// </summary>
        //public virtual ICollection<UserGroup> PersonalGroups { get; set; } = new List<UserGroup>();

        // TODO: or list of projects roles : in different projects - different roles
        //public virtual ICollection<ProjectRole> ProjectRoles { get; set; } = new List<ProjectRole>();

    }
}
