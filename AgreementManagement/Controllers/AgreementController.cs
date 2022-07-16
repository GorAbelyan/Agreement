using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgreementManagement.Controllers
{
    public class AgreementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public AgreementController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Agreement.GetAll(includeProprties: "Product,ProductGroup,User") });
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Agreement.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }

            _unitOfWork.Agreement.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        public IActionResult Upsert(int? id)
        {
            AgreementVM agreementVM = new AgreementVM()
            {
                Agreement = new Agreement(),

                ProductGroups = _unitOfWork.ProductGroup.GetAll().Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = i.GroupCode.ToString(),
                    Value = i.Id.ToString(),
                }),

                Products = _unitOfWork.Product.GetAll().Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = i.ProductNumber.ToString(),
                    Value = i.Id.ToString(),
                }),

            };
            if (id != null)
            {
                agreementVM.ModelId = id.Value;
                agreementVM.Agreement = _unitOfWork.Agreement.GetFirstOrDefult(x => x.Id == id, "Product,ProductGroup,User");
                return View(agreementVM);
            }
            else
            {

                return View(agreementVM);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                if (agreement.Id == 0)
                {
                    var user = await _userManager.GetUserAsync(User);
                    agreement.UserId = user.Id;
                    agreement.Product = null;
                    agreement.ProductGroup = null;
                    _unitOfWork.Agreement.Add(agreement);
                }
                else
                {
                    agreement.Product = null;
                    _unitOfWork.Agreement.Update(agreement);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(agreement);
        }
    }


}

