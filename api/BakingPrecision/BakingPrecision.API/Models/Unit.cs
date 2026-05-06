namespace BakingPrecision.API.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; //ex: Cup
        public string Abbreviation { get; set; } = string.Empty; //ex: 'c'

        // Used for type of measurement Metric/US customary
        public string System { get; set; } = "Metric";
    }
}
