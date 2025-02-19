namespace HospitalManagement
{
    public class AdminStaff: Person
    {
        public string Departament { get; set; }
        public string Position { get; set; }

        public AdminStaff(string name, int age, string dni, string departament, string position) : base(name, age, dni)
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