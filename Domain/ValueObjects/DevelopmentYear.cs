namespace Domain.ValueObjects
{
    public class DevelopmentYear
    {
        private decimal? _value;

        public DevelopmentYear(int originYear, int year, decimal? value)
        {
            if(!IsValid(originYear, year))
            {
                throw new Exception($"Invalid development year parameters");
            }    

            OriginYear = originYear;
            Year = year;
            _value = value;
        }

        public int OriginYear { get; private set; }
        public int Year { get; private set; }
        public decimal Value
        {
            get { return _value ?? 0; }
            private set { Value = value; }
        }

        public static bool IsValid(int? originYear, decimal? year)
        {
            return
                originYear.HasValue && originYear != 0 &&
                year.HasValue && year != 0;
        }
    }
}
