using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorldEvents.Entities;

namespace WorldEvents.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Role")]
        public string ApplicationRoleId { get; set; }
        public List<ApplicationRole> ApplicationRoles { get; set; }
    }
}
