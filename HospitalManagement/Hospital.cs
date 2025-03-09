using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement
{
    public class Hospital
    {
        public List<Doctor> Doctors { get; set; }
        public List<Patient> Patients { get; set; }
        public List<AdminStaff> AdminStaffs { get; set; }

        public Hospital()
        {
            Doctors = new List<Doctor>();
            Patients = new List<Patient>();
            AdminStaffs = new List<AdminStaff>();
        }

        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
        }

        public bool TryRemoveDoctor(string doctorDniToRemove, out string error)
        {
            Doctor doctorFound = Doctors.Find(doctor => doctor.Dni == doctorDniToRemove);

            if(doctorFound is null)
            {
                error = "This doctor is not registered";
                return false;
            }

            Doctors.Remove(doctorFound);
            error = "";
            return true;
        }

        public bool TryAddPatient(Person person, Doctor doctorAssigned, out string error)
        {
            if (doctorAssigned == null)
            {
                error = "❌ No doctor found with that DNI. Try again.";
                return false;
            }

            // Check if this patient is already assigned to this doctor
            if (doctorAssigned.Patient.Any(p => p.Dni == person.Dni))
            {
                error = "❌ This patient is already assigned to this doctor. Select another one.";
                return false;
            }

            error = "";
            // Create the patient
            Patient newPatient = new Patient(person.Name, person.Age, person.Dni, doctorAssigned);

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
            return Patients.Find(p => p.Dni == dni);
        }

        public Doctor GetDoctor(string docId)
        {
            return Doctors.Find(d => d.Dni == docId);
        }

        public void AddAdmin_staff(AdminStaff person)
        {
            AdminStaffs.Add(person);

        }
        public Person FindPersonByDni(string dni)
        {
            Doctor doctorToModify = Doctors.Find(d => d.Dni == dni);
            if (doctorToModify != null) return doctorToModify;

            Patient patientToModify = Patients.Find(p => p.Dni == dni);
            if (patientToModify != null) return patientToModify;

            AdminStaff adminStaffToModify = AdminStaffs.Find(a => a.Dni == dni);
            if (adminStaffToModify != null) return adminStaffToModify;

            return null;
            
        }

        public Person ModifyPerson(string personDni, Person person, out string error)
        {
            Person personToModify = FindPersonByDni(personDni);

            if (personToModify == null)
            {
                error = "Person not found.";
                return null; 
            }

            personToModify.Name = person.Name;

            personToModify.Age = person.Age;

            personToModify.Dni = person.Dni;

            error = "";

            return personToModify;  
        }

        public bool TryModifyDoctor(string doctorDni, Doctor doctorWithChanges, out Doctor modifiedDoctor, out string error)
        {
            Doctor doctorToModify = GetDoctor(doctorDni);

            if(doctorToModify is null)
            {
                modifiedDoctor = null;
                error = "Doctor not found";
                return false;
            }

            doctorToModify.Name = doctorWithChanges.Name;
            doctorToModify.Dni = doctorWithChanges.Dni;
            doctorToModify.Specialty = doctorWithChanges.Specialty;
            doctorToModify.Age = doctorWithChanges.Age;
            doctorToModify.CollegedNumber = doctorWithChanges.CollegedNumber;
            modifiedDoctor = doctorToModify;
            error = "";
            return true;
        }

        public bool TryModifyPatient(string patientDni, Patient patientWithChanges, out Patient modifiedPatient, out string error)
        {
            Patient patientToModify = GetPatient(patientDni);

            if(patientToModify is null)
            {
                modifiedPatient = null;
                error = "Patient not found";
                return false;
            }

            patientToModify.Name = patientWithChanges.Name;
            patientToModify.Dni = patientWithChanges.Dni;
            patientToModify.Age = patientWithChanges.Age;
            patientToModify.DoctorAssigned = patientWithChanges.DoctorAssigned;
            patientToModify.AdmissionDate = patientWithChanges.AdmissionDate;
            modifiedPatient = patientToModify;
            error = "";
            return true;
        }

        //public bool TryAddAppointment(Patient patient, Appointment appointment)
        //{
        //    patient.ClinicalHistory.Appointments.Add(appointment);
        //    return true;
        //}

        //public Appointment GetAppointment(string patientDni)
        //{
        //    // Search patient in the patient's list
        //    Patient patient = GetPatient(patientDni);
           
        //  Appointment appointment= patient.ClinicalHistory.Appointments.Find(a => a.Patient.Dni == patientDni);
        //  return appointment;
        //}
        
        //public Appointment ModifyAppointment(Appointment appointmentToModify, Func<DateTime> GetAppointmentDate)
        //{
        //    Console.WriteLine($"Current Appointment to modify: {appointmentToModify.Date:dd-MM-yyyy HH:mm} with Doctor {appointmentToModify.Doctor.Name}");
        //    Console.WriteLine("$ ⚠️ Leave an empty space to remain unchanged");
            
        //    // Get new date
        //    appointmentToModify.Date = GetAppointmentDate();
            
        //    // Get new doctor
        //    Console.WriteLine("Enter the new doctor's name:");
        //    string newDoctorName = Console.ReadLine();

        //    if (!string.IsNullOrEmpty(newDoctorName))
        //    {
        //        appointmentToModify.Doctor.Name = newDoctorName;
        //    }
        //    return appointmentToModify;  
            
        //}

        //public bool TryRemoveAppointment(Appointment appointmentToRemove, Patient patient)
        //{
        //    if (patient.ClinicalHistory.Appointments.Contains(appointmentToRemove))
        //    {
        //        patient.ClinicalHistory.Appointments.Remove(appointmentToRemove);
        //    }
        //    return true;
        //}

    }
}

