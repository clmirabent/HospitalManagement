namespace HospitalManagement
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string dni { get; set; }

        public Person(string name, int age, string dni)
        {
            Name = name;
            Age = age;
            dni = dni;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, DNI: {dni}";
        }
    }
}