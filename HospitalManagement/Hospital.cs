using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement
{
    public class Hospital
    {
        public List<Doctor> Doctors { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Admin_staff> Admin_staffs { get; set; }

        public Hospital()
        {
            Doctors = new List<Doctor>();
            Patients = new List<Patient>();
            Admin_staffs = new List<Admin_staff>();
        }

        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
        }

        public bool TryAddPatient(Person person, Doctor doctorAssigned, out string error)
        {
            if (doctorAssigned == null)
            {
                error = "❌ No doctor found with that DNI. Try again.";
                return false;
            }

            // Check if this patient is already assigned to this doctor
            if (doctorAssigned.Patient.Any(p => p.dni == person.dni))
            {
                error = "❌ This patient is already assigned to this doctor. Select another one.";
                return false;
            }

            error = "";
            // Create the patient
            Patient newPatient = new Patient(person.Name, person.Age, person.dni, doctorAssigned);

            // Add patient to hospital and to the doctor's list
            Patients.Add(newPatient);
            doctorAssigned.Patient.Add(newPatient);

            return true;
        }

        public bool TryRemovePatient(Patient patientToRemove, out Doctor doctorAssigned, out string errorMessage)
        {
            errorMessage = string.Empty;
            doctorAssigned = null;

            if (patientToRemove == null)
            {
                errorMessage = "❌ No patient found with that DNI. Try again.";
                return false;
            }

            // Buscar al doctor que tiene asignado al paciente
            doctorAssigned = Doctors.Find(d => d.Patient.Contains(patientToRemove));
            
            if (doctorAssigned != null)
            {
                // Eliminar el paciente de la lista del doctor
                doctorAssigned.Patient.Remove(patientToRemove);
            }

            // Eliminar al paciente de la lista del hospital
            Patients.Remove(patientToRemove);

            return true;
        }

        public Patient GetPatient(string dni)
        {
            return Patients.Find(p => p.dni == dni);
        }

        public Doctor GetDoctor(string docId)
        {
            while (!Doctors.Any(d=>d.dni == docId))
            {
                Console.WriteLine("Selection Invalid, please select a valid doctor");
                docId = Console.ReadLine();
            }
            return Doctors.Find(d => d.dni == docId);
        }
        public void AddAdmin_staff(Admin_staff person)
        {
            Admin_staffs.Add(person);
          
        }

        public void DisplayDoctorsPatients(string doctorDni)
        {
           

        }
    }
}