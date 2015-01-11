using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthMate.Service;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace HealthMate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to HealthMate";

            return View();
        }

        public ActionResult Search(string q)
        {
            TempData["searchString"] = q;
            var symptomService = new SymptomService();
            if (string.IsNullOrEmpty(q))
            {
                return View(symptomService.GetAll());
            }
            return View(symptomService.SearchSymptoms(q));
        }

        public ActionResult RecommendDoctors(int symptomId = -1)
        {
            try
            {
                var doctorService = new DoctorService();
                var doctors = doctorService.RecommendBySymptom(symptomId);
                return View("RecommendedDoctors", doctors);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(null, e.Message);
                return RedirectToAction("Search", new { q = TempData["searchString"] });
            }
        }

        public ActionResult ViewDoctor(int doctorId = -1)
        {
            try
            {
                var doctorService = new DoctorService();
                var doctor = doctorService.GetById(doctorId);
                return View(doctor);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(null, e.Message);
                return RedirectToAction("Search");
            }
        }

        public ActionResult Schedule(int doctorId = -1)
        {
            var doctorService = new DoctorService();
            try
            {
                ViewBag.DoctorId = doctorId;
                var doctor = doctorService.GetById(doctorId);
                var schedule = GetShedule(doctorId);
                if (schedule == null)
                {
                    throw new Exception();
                }
                return View(schedule);
            }
            catch (Exception)
            {
                ModelState.AddModelError(null, "Couldn't get the schedule");
                return RedirectToAction("ViewDoctor", new { doctorId = doctorId });
            }
        }

        public ActionResult ReserveAppointment(string hour, string day, int doctorId)
        {
            try
            {
                MakeReservation(hour, day, doctorId);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(null, "Appointment couldn't be made.");
                return RedirectToAction("ViewDoctor", new { doctorId = doctorId });
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private Dictionary<string, Dictionary<string, int>> GetShedule(int doctorId)
        {
            using (var webClient = new System.Net.WebClient())
            {
                var jsonData = string.Empty;
                try
                {
                    string url = "http://56c209f2.ngrok.com/HealthMaterReservations/getHours.php?doctor_id=" + doctorId;
                    jsonData = webClient.DownloadString(url);
                }
                catch (Exception) { }
                return !string.IsNullOrEmpty(jsonData)
                    ? JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(jsonData)
                    : null;
            }
        }

        private void MakeReservation(string hour, string day, int doctorId)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                var pairs = new NameValueCollection() {
                    { "hour", hour },
                    { "day", day },
                    {"doctor_id", doctorId.ToString()}
                };
                client.UploadValues("http://56c209f2.ngrok.com/HealthMaterReservations/reserve.php", pairs);
            }
        }
    }
}
