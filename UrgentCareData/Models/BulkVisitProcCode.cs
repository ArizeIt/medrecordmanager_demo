namespace UrgentCareData.Models
{
    public partial class BulkVisitProcCode
    {
        public string ProcCode { get; set; }
        public int? Quantity { get; set; }
        public int VisitId { get; set; }
        public int BulkVisitProcCodeId { get; set; }
        public string Modifier { get; set; }

        public virtual BulkVisit BulkVisit { get; set; }
    }
}
