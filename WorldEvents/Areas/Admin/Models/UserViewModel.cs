using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorldEvents.Entities;

namespace WorldEvents.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public List<ApplicationRole> ApplicationRoles { get; set; }

        [Display(Name = "Role")]
        public string ApplicationRoleId { get; set; }
    }
}
