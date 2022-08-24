using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Triangle
    {
        public Triangle(string name, IEnumerable<DevelopmentYear> developmentYears)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentOutOfRangeException(nameof(name)) : name;
            DevelopmentYears = developmentYears?.ToList() ?? throw new ArgumentNullException(nameof(developmentYears));
        }

        public string Name { get; set; }
        public IReadOnlyCollection<DevelopmentYear> DevelopmentYears { get; set;}
    }
}
