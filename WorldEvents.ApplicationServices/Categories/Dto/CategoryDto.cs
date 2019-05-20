using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using AutoMapper;
using WorldEvents.Entities;

namespace WorldEvents.Categories.Dto
{
    [AutoMap(typeof(Category))]
    public class CategoryDto : FullAuditedEntityDto<long>
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        //public virtual ICollection<CategoryPermissionDto> Permissions { get; set; } = new List<CategoryPermissionDto>();
    }
}