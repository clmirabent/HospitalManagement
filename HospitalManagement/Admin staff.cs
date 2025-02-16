namespace HospitalManagement
{
    public class Admin_staff: Person
    {
        public string Departament { get; set; }
        public string Position { get; set; }

        public Admin_staff(string name, int age, string dni, string departament, string position) : base(name, age, dni)
        {
            Departament = departament;
            Position = position;
        }

        public override string ToString()
        {
            return $"ADMIN STAFF - {base.ToString()}, Department: {Departament}, Position: {Position}";
        }
        
    }
}