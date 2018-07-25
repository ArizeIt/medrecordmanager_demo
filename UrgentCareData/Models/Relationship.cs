using System;
using System.Collections.Generic;

namespace UgentCareDate.Models
{
    public partial class Relationship
    {
        public string Description { get; set; }
        public string Hipaarelationship { get; set; }
        public int? AmrelationshipCode { get; set; }
    }
}
