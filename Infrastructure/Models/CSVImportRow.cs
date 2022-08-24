namespace Infrastructure.Models
{
    public class CSVImportRow
    {
        public string Name { get; set; }
        public int OriginYear { get; set; }
        public int Year { get; set; }
        public decimal Value { get; set; }
    }
}
