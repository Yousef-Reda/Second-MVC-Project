using Final.Models;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    public class BranchController : Controller
    {
        private readonly PharmacyContext pharmacyContext;
        public BranchController(PharmacyContext context)
        {
            pharmacyContext = context;
        }
        public IActionResult Index()
        {
            ICollection<Branch> Branches = pharmacyContext. Branches.ToList();
            return View(Branches);
        }
        public IActionResult Details(int id)
        {
            var branch = pharmacyContext.Branches.FirstOrDefault(b => b.Bid == id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }
      

    }
}
