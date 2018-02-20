using Microsoft.AspNetCore.Identity;
using System;

namespace WorldEvents.Models
{
    public class ApplicationRole : IdentityRole//<Guid>
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
    }
}
