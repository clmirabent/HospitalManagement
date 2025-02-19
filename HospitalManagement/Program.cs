using System;
using System.Globalization;
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
8️⃣ Create appointment"
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
                                Console.WriteLine(
                                    $"✅ Patient {patientToRemove.Name} removed from Dr. {doctorUnassigned.Name}'s list.");
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
                            AdminStaff newAdminStaff = CreateAdminStaff(personAdmin);
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
                        case "8":
                            Console.WriteLine("---YOU ARE GOING TO CREATE AN APPOINTMENT---");
                            Patient patientToAddAppointment = GetPatient(hospital);
                            Doctor doctorForAppointment;

                            if (patientToAddAppointment == null)
                            {
                                Console.WriteLine(
                                    @"❌This patient don't exist. ---YOU ARE GOING TO ADD A NEW PATIENT---");
                                Person personToCreate = CreatePerson();
                                Console.WriteLine("---Select a doctor by typing their dni number:---");
                                Doctor doctorAssigned = GetDoc(hospital);
                                if (!hospital.TryAddPatient(personToCreate, doctorAssigned, out string error1))
                                {
                                    Console.WriteLine(error1);
                                }
                                else
                                {
                                    Console.WriteLine(
                                        $"✅Patient {personToCreate.Name} added successfully to Dr. {doctorAssigned.Name}.");
                                }

                                patientToAddAppointment = hospital.GetPatient(personToCreate.Dni);
                                doctorForAppointment = doctorAssigned;
                            }
                            else
                            {
                                Console.WriteLine("---Select a doctor by typing their dni number:---");
                                doctorForAppointment = GetDoc(hospital);
                            }

                            DateTime appointmentDate = GetAppointmentDate();

                            Appointment newAppointment = new Appointment(patientToAddAppointment, appointmentDate,
                                doctorForAppointment);
                            hospital.TryAddAppointment(patientToAddAppointment, newAppointment);
                            Console.WriteLine(
                                $"✅ Appointment scheduled for {patientToAddAppointment.Name} with Dr. {doctorForAppointment.Name} on {appointmentDate}.");
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
            Console.Write("Enter doctor's dni:");
            string docId = Console.ReadLine();

            Doctor doctorToAsign = hospital.GetDoctor(docId);

            while (doctorToAsign == null)
            {
                Console.Write("Enter doctor's dni:");
                docId = Console.ReadLine();
                doctorToAsign = hospital.GetDoctor(docId);
            }

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
            Console.WriteLine("Please enter the patient's dni: ");
            string inputDni = Console.ReadLine();

            // Find the patient by DNI
            Patient patientToRemove = hospital.GetPatient(inputDni);

            return patientToRemove;
        }

        static AdminStaff CreateAdminStaff(Person person)
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

            AdminStaff newAdminStaff = new AdminStaff(person.Name, person.Age, person.Dni, department, position);
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

        static DateTime GetAppointmentDate()
        {
            DateTime appointmentDate;

            while (true) // Keep asking until the user enters a valid date
            {
                Console.WriteLine("Enter appointment date and time (format: dd-MM-yyyy HH:mm):");
                string dateTimeInput = Console.ReadLine();

                if (DateTime.TryParseExact(dateTimeInput, "dd-MM-yyyy HH:mm",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out appointmentDate))
                {
                    return appointmentDate; // Return the valid date
                }
                else
                {
                    Console.WriteLine("❌ Invalid date format. Please try again.");
                }
            }
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