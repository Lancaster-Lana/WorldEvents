using Abp.Authorization.Users;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldEvents.Entities
{
    /// <summary>
    /// UserProfile belong for some ApplicationUser.
    /// </summary>
    public class UserProfile : FullAuditedEntity<long> //AbpUser<UserProfile>  
    {
        /// <summary>
        /// Parent user entity
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ' ' + FirstName;
            }
        }

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