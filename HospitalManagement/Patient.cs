using System;

namespace HospitalManagement
{
    public class Patient: Person
    {
        public Doctor DoctorAssigned { get; set; }
        
        public DateTime AdmissionDate { get; set; }
        public Clinical_history Clinical_history { get; set; }
        
        
        public Patient(string name, int age, string dni, Doctor doctorAssigned) : base(name, age, dni)
        {
            DoctorAssigned = doctorAssigned;
            AdmissionDate = DateTime.Now;
            Clinical_history = new Clinical_history();
        }
        
        public override string ToString()
        {
            return $"PATIENT - {base.ToString()}, Doctor Assigned: {DoctorAssigned.Name}, Admission Date: {AdmissionDate.ToShortDateString()}";
        }

        
    }
}