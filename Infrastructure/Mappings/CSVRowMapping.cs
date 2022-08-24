using Infrastructure.Models;
using TinyCsvParser.Mapping;

namespace Infrastructure.Mappings
{
    public class CSVRowMapping : CsvMapping<CSVImportRow>
    {
        public CSVRowMapping()
            : base()
        {
            MapProperty(0, x => x.Name);
            MapProperty(1, x => x.OriginYear);
            MapProperty(2, x => x.Year);
            MapProperty(3, x => x.Value);
        }
    }
}
