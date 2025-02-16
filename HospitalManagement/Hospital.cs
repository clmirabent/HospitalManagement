using System.Collections.Generic;

namespace HospitalManagement
{
    public class Hospital
    {
        public List<Person> Persons { get; set; }
        

        public Hospital()
        {
            Persons = new List<Person>();
        }
    }
}