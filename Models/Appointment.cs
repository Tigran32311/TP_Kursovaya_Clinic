    namespace Clinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public int Status { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int timeId { get; set; }
        public Time time { get; set; }

    }
}
