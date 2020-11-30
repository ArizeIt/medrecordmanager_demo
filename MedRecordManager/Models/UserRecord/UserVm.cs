using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models.UserRecord
{
    public class UserVm
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Roles { get; set; }
        public string Companies { get; set; }

        public string OfficeKeys { get; set; }

        public string Clinics { get; set; }

        public string Role { get; set; }

        public IEnumerable<SelectListItem> AssignedRoles { get; set; }
        public IEnumerable<SelectListItem> AvaliableRoles { get; set; }
        public IEnumerable<SelectListItem> AssignedComps { get; set; }
        public IEnumerable<SelectListItem> AvaliableComps { get; set; }
        public IEnumerable<SelectListItem> AssignedOffices { get; set; }
        public IEnumerable<SelectListItem> AvaliableOffices { get; set; }
        public IEnumerable<SelectListItem> AssignedClinics { get; set; }
        public IEnumerable<SelectListItem> AvailableClinics { get; set; }

        public string UserId { get; set; }
        public FilterUser Filter { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool HasError { get; set; }
    }

}
