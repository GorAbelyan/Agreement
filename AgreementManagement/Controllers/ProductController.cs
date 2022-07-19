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
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var product = _unitOfWork.Product.GetAll(includeProprties: "ProdGroup").ToList();
            return Json(new { data = product });
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }

            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        public IActionResult Upsert(int? id)
        {
            var productGroups = _unitOfWork.ProductGroup.GetAll();
            var products = _unitOfWork.Product.GetAll();

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                ProductGroups = new SelectList(productGroups, nameof(ProductGroup.Id), nameof(ProductGroup.GroupCode)),

            };
            if (id != null)
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefult(x => x.Id == id, "ProdGroup");
                return View(productVM);
            }
            else
            {
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {

                    _unitOfWork.Product.Add(product);
                }
                else
                {
                    _unitOfWork.Product.Update(product);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }


}

