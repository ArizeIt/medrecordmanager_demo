﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models.DailyRecord
{
    public class DetailRecord
    {
        public List<ChartDoc> VisitCharts { get; set; }

        public int ChartId { get; set; }
        public Guarantor GuarantorInfo { get; set; }

        public string VisitId { get; set; }

        public IEnumerable<Insurance> InsuranceInfo { get; set; }

        public Patient PaitentInfo{ get; set; }

        public IList<SelectListItem> Relationship { get; set; }
    }

    public class Guarantor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [StringLength(50)]
        public string MidName { get; set; }

        public int PayerNum { get; set; }

        public Address Address { get; set; }

        public string phone { get; set; }

        public string RelationshipCode { get; set; }

    }

    public class ChartDoc
    {
        public string FileName { get; set; }

        public int ChartDocId { get; set; }
    }

    public class Patient
    {
        [Required(ErrorMessage ="First Name is Reuried")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Reuried")]
        [StringLength(50)]
        public string LastName { get; set; }

        
        [StringLength(50)]
        public string MidName { get; set; }

        public string Dob { get; set; }

        [Required(ErrorMessage = "SSN is Required")]
        [RegularExpression(@"^\d{9}|\d{3}-\d{2}-\d{4}$", ErrorMessage = "Invalid Social Security Number")]
        public string SSN { get; set; }


        public string Email { get; set; }

        public Address Address { get; set; }
       

        public string CellPhone { get; set; }

        public string HomePhone { get; set; }

        public int PvNumber { get; set; }
    }

    public class Insurance
    {
        public string InsuranceName { get; set; }

        public Address Address { get; set; }

        public string Phone { get; set; }

        public string AmdCode { get; set; }
    }

    public class Address
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        public string Zip { get; set; }

       
    }
}   



