namespace Clinic.Models
{
    public class Сonclusion
    {
        public int Id { get; set; }
        public string Complaints { get;set; }
        public string Recommendations { get; set; }
        public string Diagnosis { get; set; }
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
