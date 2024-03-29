﻿using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModel;
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
            var productGroups = _unitOfWork.ProductGroup.GetAll();
            var products = _unitOfWork.Product.GetAll();

            AgreementVM agreementVM = new AgreementVM()
            {
                Agreement = new Agreement(),

                ProductGroups = new SelectList(productGroups, nameof(ProductGroup.Id), nameof(ProductGroup.GroupCode)),
                Products = new SelectList(products, nameof(Product.Id), nameof(Product.ProductNumber)),
             
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

