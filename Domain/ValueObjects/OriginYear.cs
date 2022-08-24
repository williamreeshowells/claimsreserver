namespace Domain.ValueObjects
{
    public class OriginYear
    {
        public OriginYear(int value, IEnumerable<DevelopmentYear> developmentYears)
        {
            Value = value;
            DevelopmentYears = developmentYears ?? throw new ArgumentNullException(nameof(developmentYears));
        }

        public int Value { get; private set; }
        public IEnumerable<DevelopmentYear> DevelopmentYears { get; private set; }
    }
}
