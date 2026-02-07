namespace WebApplication1.Entities
{
    public class DashboardRecord
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; } = null!;
        public string PropertyReference { get; set; } = null!;
        public string PropertyName { get; set; } = null!;
        public string PostCode { get; set; } = null!;
        public string Assessor { get; set; } = null!;
        public string Reviewer { get; set; } = null!;
        public DateTime NextExpectedEvent { get; set; }
    }

}