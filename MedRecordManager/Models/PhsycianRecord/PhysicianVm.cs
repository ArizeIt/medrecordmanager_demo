﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedRecordManager.Models
{
    public class PhysicianVm
    {
        [Required]
        public int? pvPhysicianId { get; set; }

        [Required]
        public string pvFirstName { get; set; }

        [Required]
        public string pvLastName { get; set; }

        public string AmdProviderCode { get; set; }

        public string AmdProfileId { get; set; }

        [Required]
        public string AmdDisplayName { get; set; }

        public IEnumerable<SelectListItem> MappedProviders { get; set; }

        public bool IsDefault { get; set; }

        public SearchInputs Inputs { get; set; }
    }
}
