namespace AngularMvc6.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? ZoneId { get; set; }
        public string Url { get; set; }
        public int LocationType { get; set; }
        public int? ParentStationId { get; set; }
    }
}