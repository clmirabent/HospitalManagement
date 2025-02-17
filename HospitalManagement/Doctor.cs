using System.Collections.Generic;

namespace HospitalManagement
{
    public class Doctor: Person
    {
        public string Specialty { get; set; }
        public string CollegedNumber { get; set; }
        public List<Patient> Patient { get; set; }
        
        
        public Doctor(string name, int age, string dni, string specialty, string collegedNumber) : base(name, age, dni)
        {
            Specialty = specialty;
            CollegedNumber = collegedNumber;
            Patient = new List<Patient>();  
        }
    }
}