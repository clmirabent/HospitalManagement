namespace HospitalManagement
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Dni { get; set; }

        public Person(string name, int age, string dni)
        {
            Name = name;
            Age = age;
            Dni = dni;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, DNI: {Dni}";
        }
    }
}