namespace UrgentCareData.Models
{
    public partial class VisitProcCode
    {
        public string ProcCode { get; set; }
        public int? Quantity { get; set; }
        public int VisitId { get; set; }
        public int VisitProcCodeId { get; set; }
        public string Modifier { get; set; }

        public virtual Visit Visit { get; set; }
    }
}
