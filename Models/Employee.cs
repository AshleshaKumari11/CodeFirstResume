using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmpResumeBuilder.Models
{
    public class Employee
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Mobile Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                  ErrorMessage = "Entered Mobile No. format is not valid.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email is Required !")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                       @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                       @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                       ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Technical Skill is required !")]
        public string TechnicalSkills { get; set; }

        [Required(ErrorMessage = "Project Detail is required !")]
        public string ProjectDetails { get; set; }

        [Required(ErrorMessage = "Experience is required !")]
        public string RelevantExperience { get; set; }

    }
}