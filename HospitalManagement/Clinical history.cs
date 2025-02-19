using System.Collections.Generic;

namespace HospitalManagement
{
    public class ClinicalHistory
    {
        public List<string> Diagnostics { get; set; }
        public List<string> Treatments { get; set; }
        public List<Appointment> Appointments { get; set; }

        public ClinicalHistory()
        { 
            Diagnostics = new List<string>();
            Treatments = new List<string>();
            Appointments = new List<Appointment>();
        }

      
        
    }
    

 
}