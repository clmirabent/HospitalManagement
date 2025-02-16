using System.Collections.Generic;

namespace HospitalManagement
{
    public class Clinical_history
    {
        public List<string> Diagnostics { get; set; }
        public List<string> Treatments { get; set; }
        public List<Appointments> Appointments { get; set; }

        public Clinical_history() 
        { 
            Diagnostics = new List<string>();
            Treatments = new List<string>();
            Appointments = new List<Appointments>();
        }
    }

    public class Appointments
    {
    }
}