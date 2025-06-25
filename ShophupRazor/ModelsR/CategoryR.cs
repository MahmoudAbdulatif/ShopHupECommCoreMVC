﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShophupRazor.ModelsR
{
    public class CategoryR
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "You Must Enter The Category Name")]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
