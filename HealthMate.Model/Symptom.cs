using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMate.Model
{
    public class Symptom
    {
        public Symptom()
        {
            this.Doctors = new HashSet<Doctor>();
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
