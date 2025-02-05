﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Student
    {
        [Column("STUDENT_ID")]
        public Guid Id { get; set; }
        [Column("FIRST_NAME")]
        [DisplayName("Name")]
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter student's name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("LAST_NAME")]
        [DisplayName("Surname")]
        [MinLength(1)]
        [Required(ErrorMessage = "Please enter student's surname")]
        public string LastName { get; set; } = string.Empty;
        [Column("GROUP_ID")]
        public Guid GroupId { get; set; }
        [ValidateNever]
        [Required(ErrorMessage = "Please select a group")]
        public Group Group { get; set; } = null!;
    }
}
