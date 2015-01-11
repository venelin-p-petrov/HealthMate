using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMate.Model
{
    public class Doctor
    {
        public Doctor()
        {
            this.Symptoms = new HashSet<Symptom>();
        }

        [Key]
        public int ID { get; set; }
        public string IdentificationNumber { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Symptom> Symptoms { get; set; }
    }
}
