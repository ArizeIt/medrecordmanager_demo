namespace MedRecordManager.Models
{
    public class CodeChartVm
    {
        public CodeChartVm()
        {
            Chart = new ChartVm();
        }
        public int VisitId { get; set; }
        public ChartVm Chart { get; set; }

        public int Position { get; set; }

        public int Total { get; set; }

        public string PhysicianName { get; set; }

        public string PhysicanEmail { get; set; }
    }

    public class ChartVm
    {
        public ChartVm()
        {
            ChartType = string.Empty;
            ChartName = string.Empty;
            FileBinary = new byte[0];
            IsFlaged = false;
        }
        public string ChartName { get; set; }
        public string ChartType { get; set; }
        public byte[] FileBinary { get; set; }

        public bool IsFlaged { get; set; }
    }



    public class Code
    {
        public int Id { get; set; }
        public string CodeType { get; set; }
        public string CodeName { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string ModifierCode { get; set; }

        public string ModifierCode2 { get; set; }
    }

}
