using System;

namespace HospitalManagement
{
    public class Patient: Person
    {
        public Doctor DoctorAssigned { get; set; }
        
        public DateTime AdmissionDate { get; set; }
        public ClinicalHistory ClinicalHistory { get; set; }
        
        
        public Patient(string name, int age, string dni, Doctor doctorAssigned) : base(name, age, dni)
        {
            DoctorAssigned = doctorAssigned;
            AdmissionDate = DateTime.Now;
            ClinicalHistory = new ClinicalHistory();
        }
        
        public override string ToString()
        {
            return $"PATIENT - {base.ToString()}, Doctor Assigned: {DoctorAssigned.Name}, Admission Date: {AdmissionDate.ToShortDateString()}";
        }

        
    }
}