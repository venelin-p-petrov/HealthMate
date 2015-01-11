using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMate.Model
{
    public class HealthMateContext : DbContext
    {
        public HealthMateContext()
            : base("name=HealthMateConnection")
        {
            Database.SetInitializer<HealthMateContext>(new DropCreateDatabaseIfModelChanges<HealthMateContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
    }
}
