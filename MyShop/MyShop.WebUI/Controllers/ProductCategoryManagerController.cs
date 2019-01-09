using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            this.context = context;
        }
        
        // GET: ProductCategory
        public ActionResult Index()
        {
            List<ProductCategory> productCategory = context.Collection().ToList();
            return View(productCategory);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {

            ProductCategory productCategory = context.Find(id);
            if (productCategory != null)
            {
                return View(productCategory);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productCategoryToEdit = context.Find(id);
            if(productCategoryToEdit != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                else
                {
                    productCategoryToEdit.Category = productCategory.Category;
                    context.Update(productCategoryToEdit);
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Delete(string id)
        {
            ProductCategory productCategoryToDelete = context.Find(id);
            if(productCategoryToDelete != null)
            {
                return View(productCategoryToDelete);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfimDelete(string id)
        {
            ProductCategory productCategoryToDelete = context.Find(id);
            if (productCategoryToDelete != null)
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}