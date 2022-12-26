namespace Clinic.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
