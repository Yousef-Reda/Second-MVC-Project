using Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final.Controllers
{
    public class MedicineController : Controller
    {
        private readonly PharmacyContext pharmacyContext;
        public  MedicineController(PharmacyContext context)
        {
            pharmacyContext = context;
        }
        public IActionResult Index()
        {
            ICollection<Medicine> medicines = pharmacyContext.Medicines.ToList();
            return View(medicines);
        }
        public ActionResult Details(int id)
        {
            var medicine = pharmacyContext.Medicines
                         .Include(m => m.BidNavigation)
                         .FirstOrDefault(m => m.Id == id);
            if (medicine == null)
            {
                Console.WriteLine("Medicine Not Found");
            }
            return View(medicine);
        }
        public IActionResult AvailableAt(int id)
        {
            var branches = pharmacyContext.Medicines
                              .Where(m => m.Id == id)
                              .Select(m => m.BidNavigation)
                              .Where(b => b != null)
                              .ToList();

            return View(branches);
        }
        public IActionResult Delete(int id)
        {
            var ins = pharmacyContext.Medicines.Where(x => x.Id == id).FirstOrDefault();
            pharmacyContext.Medicines.Remove(ins);
            pharmacyContext.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Edit(int id)
        {
            var medicine = pharmacyContext.Medicines.Where(s => s.Id == id).FirstOrDefault();
            return View(medicine);
        }
        [HttpPost]
        public IActionResult Edit(Medicine medicine)
        {
            var currentmedicine = pharmacyContext.Medicines.Where(s => s.Id == medicine.Id).FirstOrDefault();
            currentmedicine.Id = medicine.Id;
            currentmedicine.Mname = medicine.Mname;
            currentmedicine.Ingredient = medicine.Ingredient;
            currentmedicine.Price = medicine.Price;

            pharmacyContext.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                pharmacyContext.Medicines.Add(medicine);
                pharmacyContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.Message = "Please enter a medicine name.";
                return View("Index", pharmacyContext.Medicines.ToList());
            }

            var medicine = pharmacyContext.Medicines
                             .FirstOrDefault(m => m.Mname.Contains(query));

            if (medicine == null)
            {
                ViewBag.Message = "Sorry, this medicine is unavailable.";
                return View("Index", pharmacyContext.Medicines.ToList());
            }

            return RedirectToAction("Details", new { id = medicine.Id });
        }







    }
}
