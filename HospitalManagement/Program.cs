using System;
using System.Linq;

namespace HospitalManagement
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Hospital hospital = new Hospital();


            while (true)
            {
                Console.Clear();
                Console.WriteLine(@"
--- 🏥 HOSPITAL MANAGEMENT SYSTEM ---);
1️⃣ Add a Doctor
2️⃣ Add a Patient
3️⃣ Remove a Patient
4️⃣ Display Doctors
5️⃣ Display Patients of a Doctor
6️⃣ Add Admin Staff
7️⃣ Modify a data from a person
8️⃣ Exit"
                );
                while (true)
                {
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("---YOU ARE GOING TO ADD A NEW DOCTOR---");
                            Doctor newDoctorToCreate = CreateDoctor(CreatePerson());
                            Console.WriteLine($"✅ Doctor {newDoctorToCreate.Name} added successfully!");
                            hospital.AddDoctor(newDoctorToCreate);
                            break;
                        case "2":
                            Console.WriteLine("---YOU ARE GOING TO ADD A NEW PATIENT---");
                            Person personDoc = CreatePerson();
                            Console.WriteLine("---Select a doctor by typing their colleged number:---");
                            Doctor doctorToAssign = GetDoc(hospital);
                            if (!hospital.TryAddPatient(personDoc, doctorToAssign, out string error))
                            {
                                Console.WriteLine(error);
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"✅Patient {personDoc.Name} added successfully to Dr. {doctorToAssign.Name}.");
                            }
                            break;
                        case "3":
                            Console.WriteLine("---YOU ARE GOING TO REMOVE A PATIENT---");
                            Patient patientToRemove = GetPatient(hospital);
                            if (!hospital.TryRemovePatient(patientToRemove, out Doctor doctorUnassigned,
                                    out string errorMessage))
                            {
                                Console.WriteLine(errorMessage);
                            }
                            else if (doctorUnassigned != null)
                            {
                                Console.WriteLine($"✅ Patient {patientToRemove.Name} removed from Dr. {doctorUnassigned.Name}'s list.");
                            }
                            else
                            {
                                Console.WriteLine($"✅ Patient {patientToRemove.Name} removed from the hospital.");
                            }
                            break;
                        case "4":
                            Console.WriteLine("---LIST OF DOCTORS---");
                            DisplayDoctors(hospital);
                            break;
                        case "5":
                            Console.WriteLine("---LIST OF DOCTOR'S PATIENTS---");
                            DisplayDoctorsPatients(hospital);
                            break;
                        case "6":
                            Console.WriteLine("---YOU ARE GOING TO ADD A NEW ADMIN STAFF---");
                            Person personAdmin = CreatePerson();
                            Admin_staff newAdminStaff = CreateAdminStaff(personAdmin);
                            hospital.AddAdmin_staff(newAdminStaff);
                            Console.WriteLine($"✅Admin staff {newAdminStaff.Name} added successfully.");
                            break;
                        case "7":
                            Console.WriteLine("---YOU ARE GOING TO MODIFY DATA FROM A PERSON---");
                            Person personToModify = GetPerson(hospital);
                            if (personToModify != null)
                            {
                                hospital.ModifyPerson(personToModify);
                                Console.WriteLine("✅ Data updated successfully!.");
                            }
                            break;
                        default:
                            Console.WriteLine("❌ Invalid choice, please select a valid option.");
                            break;
                    }
                }
            }
        }

        static Person CreatePerson()
        {
            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();

            while (name == "")
            {
                Console.WriteLine("Enter a valid name");
                name = Console.ReadLine();
            }

            Console.WriteLine($"Enter age: ");
            int age;

            while (!(int.TryParse(Console.ReadLine(), out age) && age > 0 || age < 100))
            {
                Console.WriteLine("Enter a valid age");
            }

            Console.WriteLine($"Enter the DNI: ");
            string dni = Console.ReadLine();

            while (dni == "")
            {
                Console.WriteLine("Enter a valid dni");
                dni = Console.ReadLine();
            }
            return new Person(name, age, dni);
        }

        static Doctor CreateDoctor(Person person)
        {
            Console.WriteLine($"Enter the doctor's speciality: ");
            string specialty = Console.ReadLine();
            while (specialty == "")
            {
                Console.WriteLine("Enter a valid specialty");
                specialty = Console.ReadLine();
            }

            Console.WriteLine($"Enter the doctor's colleged number: ");
            string collegedNumber = Console.ReadLine();
            while (collegedNumber == "")
            {
                Console.WriteLine("Enter a valid colleged number");
                collegedNumber = Console.ReadLine();
            }
            // Create and add the doctor
            return new Doctor(person.Name, person.Age, person.Dni, specialty, collegedNumber);
        }

        static Doctor GetDoc(Hospital hospital)
        {
            DisplayDoctors(hospital);
            string docId = Console.ReadLine();

            Doctor doctorToAsign = hospital.GetDoctor(docId);

            return doctorToAsign;
        }

        static void DisplayDoctors(Hospital hospital)
        {
            foreach (var doctor in hospital.Doctors)
            {
                Console.WriteLine(
                    $"Name: {doctor.Name}, DNI: {doctor.Dni}, Specialty: {doctor.Specialty}, Colleged Number: {doctor.CollegedNumber}");
            }
        }

        static void DisplayDoctorsPatients(Hospital hospital)
        {
            Doctor doctorListToShow = GetDoc(hospital);

            if (doctorListToShow == null)
            {
                Console.WriteLine("❌ No doctor found with that DNI. Try again.");
                return;
            }

            // Check if the doctor has any patients
            if (doctorListToShow.Patient.Count == 0)
            {
                Console.WriteLine("❌ No patients assigned to this doctor.");
                return;
            }

            // Display list of patients for the doctor
            Console.WriteLine($"\nPatients assigned to Dr. {doctorListToShow.Name}:");
            foreach (var patient in doctorListToShow.Patient)
            {
                Console.WriteLine($"- {patient.Name}, DNI: {patient.Dni}, Age: {patient.Age}");
            }
        }

        static Patient GetPatient(Hospital hospital)
        {
            Console.WriteLine("Please enter the patient's dni you want to remove: ");
            string inputDni = Console.ReadLine();

            // Find the patient by DNI
            Patient patientToRemove = hospital.GetPatient(inputDni);

            return patientToRemove;
        }

        static Admin_staff CreateAdminStaff(Person person)
        {
            Console.WriteLine("Add the department for this staff");
            string department = Console.ReadLine();

            while (department == "")
            {
                Console.WriteLine("Enter a valid department");
            }

            Console.WriteLine("Add the position for this staff");
            string position = Console.ReadLine();
            while (position == "")
            {
                Console.WriteLine("Enter a valid position");
            }

            Admin_staff newAdminStaff = new Admin_staff(person.Name, person.Age, person.Dni, department, position);
            return newAdminStaff;
        }

        static Person GetPerson(Hospital hospital)
        {
            Console.WriteLine("Please enter the person's dni you want to modify: ");
            string inputDni = Console.ReadLine();

            // Find the person by DNI
            Person personToModify = hospital.FindPersonByDni(inputDni);
            return personToModify;
        }
        

        public static int ConvertStringInt(string question)
        {
            while (true)
            {
                Console.Write(question);
                if (!int.TryParse(Console.ReadLine(), out int number))
                    Console.WriteLine("Convertion failed.");
                else
                    return number;
            }
        }
    }
}