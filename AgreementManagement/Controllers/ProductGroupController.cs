using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgreementManagement.Controllers
{
    public class ProductGroupController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductGroupController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var productGroup = _unitOfWork.ProductGroup.GetAll();
            return Json(new { data = productGroup });
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.ProductGroup.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }

            _unitOfWork.ProductGroup.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        public IActionResult Upsert(int? id)
        {
            var productGroups = _unitOfWork.ProductGroup.GetAll();

            ProductGroup productGroup = new ProductGroup();

            if (id != null)
            {
                productGroup = _unitOfWork.ProductGroup.GetFirstOrDefult(x => x.Id == id);
                return View(productGroup);
            }
            else
            {
                return View(productGroup);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Upsert(ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                if (productGroup.Id == 0)
                {
                    _unitOfWork.ProductGroup.Add(productGroup);
                }
                else
                {
                    _unitOfWork.ProductGroup.Update(productGroup);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(productGroup);
        }
    }


}

