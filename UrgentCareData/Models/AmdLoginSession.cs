﻿using System;
using System.Collections.Generic;

namespace UrgentCareData.Models
{
    public partial class AmdLoginSession
    {
        public string Context { get; set; }
        public string ApiUrl { get; set; }
        public DateTime Born { get; set; }
        public int Id { get; set; }
    }
}