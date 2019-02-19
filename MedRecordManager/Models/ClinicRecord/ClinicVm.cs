using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Models.ClinicRecord
{
    public class ClinicVm
    {
        public SearchInputs Input { get; set; }

        public string PVClinicName { get; set; }

        public string AmdFircilityName { get; set; }

        public List<int > OfficeKeys { get; set; }
    }
}
