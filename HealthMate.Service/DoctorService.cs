using HealthMate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMate.Service
{
    public class DoctorService
    {
        public IEnumerable<Doctor> RecommendBySymptom(int symptomId)
        {
            using (var db = new HealthMateContext())
            {
                var symptom = db.Symptoms.Where(s => s.ID == symptomId).FirstOrDefault();
                if (symptom == null)
                {
                    throw new ArgumentException("Symptom can't be found");
                }
                return symptom.Doctors;
            }
        }

        public Doctor GetById(int id)
        {
            using (var db = new HealthMateContext())
            {
                var doctor = db.Doctors.Where(d => d.ID == id).FirstOrDefault();
                if (doctor == null)
                {
                    throw new ArgumentException("Doctor can't be found");
                }
                return doctor;
            }
        }

        public Doctor GetByIdentificationNumber(string idNumber)
        {
            using (var db = new HealthMateContext())
            {
                var doctor = db.Doctors.Where(d => d.IdentificationNumber == idNumber).FirstOrDefault();
                if (doctor == null)
                {
                    throw new ArgumentException("Doctor can't be found");
                }
                return doctor;
            }
        }
    }
}
