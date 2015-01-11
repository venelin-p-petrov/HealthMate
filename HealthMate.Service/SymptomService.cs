using HealthMate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMate.Service
{
    public class SymptomService
    {
        public IEnumerable<Symptom> SearchSymptoms(string query)
        {
            using (var db = new HealthMateContext())
            {
                var symptoms = db.Symptoms.Where(s => s.Name.Contains(query)).ToList();
                return symptoms;
            }
        }

        public IEnumerable<Symptom> GetAll()
        {
            using (var db = new HealthMateContext())
            {
                return db.Symptoms.ToList();
            }
        }
    }
}
