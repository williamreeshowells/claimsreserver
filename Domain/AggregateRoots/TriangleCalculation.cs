using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.AggregateRoots
{
    public class TriangleCalculation
    {
        public TriangleCalculation(IReadOnlyCollection<Triangle> triangles)
        {
            Triangles = triangles ?? throw new ArgumentNullException(nameof(triangles));
        }

        public IReadOnlyCollection<Triangle> Triangles { get; set; }
        public int EarliestOriginYear => Triangles
            .SelectMany(x => x.DevelopmentYears)
            .Min(x => x.OriginYear);

        public int NumberOfOriginYears => Triangles
            .SelectMany(x => x.DevelopmentYears)
            .Select(x => x.OriginYear)
            .Distinct()
            .Count();

        public IEnumerable<OriginYear> OriginYears
        {
            get
            {
                return Triangles.SelectMany(x => x.DevelopmentYears)
                .GroupBy(x => x.OriginYear)
                .Select(x => new
                {
                    OriginYear = x.First().OriginYear,
                    MaxDevelopmentYear = x.Select(x => x.Year).Max(),
                    MinDevelopmentYear = x.Select(x => x.Year).Min(),
                })
                .Select(x => new OriginYear(
                    value: x.OriginYear,
                    developmentYears: Enumerable.Range(x.MinDevelopmentYear, (x.MaxDevelopmentYear - x.MinDevelopmentYear) + 1)
                    .Select(year => new DevelopmentYear(x.OriginYear, year, null)))
                {
                })
                .OrderBy(x => x.Value);
            }
        }

        public int TotalDevelopmentalYears => OriginYears.SelectMany(x => x.DevelopmentYears).Count();
    }
}
