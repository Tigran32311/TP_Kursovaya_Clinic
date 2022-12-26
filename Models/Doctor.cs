namespace Clinic.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fathername { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //смена
        public int WorkShift { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
