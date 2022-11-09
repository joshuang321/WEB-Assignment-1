using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using web_asignment1_Y2S1_2022.Models;

namespace web_asignment1_Y2S1_2022.Controllers
{
    public class ProductManagerController : Controller
    {
        //Only a user with a role of ProductManager can access this /ProductManager/Index & /ProductManager/CreateProduct
        public IActionResult Index()
        {
            Console.WriteLine("Index");
            if (HttpContext.Session.GetString("Role") != null && HttpContext.Session.GetString("Role") == string.Empty)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Product Manager"))
                {
                    ViewData["Page"] = "ViewProduct";
                    return View(Product.getProducts());
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        public IActionResult CreateProduct()
        {
            if (HttpContext.Session.GetString("Role") != null && HttpContext.Session.GetString("Role") == string.Empty)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Product Manager"))
                {
                    ViewData["Page"] = "CreateProduct";
                    return View(new CreateProductViewModel());
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(RenovatedProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                if (null != productVM.fileToUpload &&
                productVM.fileToUpload.Length > 0)
                {
                    string fileExt = Path.GetExtension(productVM.fileToUpload.FileName);
                    // Rename the uploaded file with the staff’s name.
                    string uploadedFile = productVM.ProductName + fileExt;
                    // Get the complete path to the images folder in server
                    string savePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot\\images", uploadedFile);
                    // Upload the file to server
                    using (var fileSteam = new FileStream(
                    savePath, FileMode.Create))
                    {
                        await productVM.fileToUpload.CopyToAsync(fileSteam);
                    }
                    productVM.ProductImage = uploadedFile;
                }
                else if (null != productVM.fileToUpload &&
                    0 == productVM.fileToUpload.Length)
                {
                    productVM.ProductImage = null;
                }
                productVM.addNewProduct();
                return RedirectToAction("Index");
            }
            ViewData["Page"] = "CreateProduct";
            return View(productVM);
        }

        // Update Product displayed based on productId
        public ActionResult UpdateProduct(int? productId)
        {
            if (null == productId)
            {
                return RedirectToAction("Index");
            }
            UpdateProductViewModel productVM = new UpdateProductViewModel(productId.Value);
            if (null == productVM)
            {
                return RedirectToAction("Index");
            }
            TimeSpan timeSpan = DateTime.Now - productVM.EffectiveDate;
            ViewData["Page"] = "UpdateProduct";
            ViewData["ShowisObsolete"] = (timeSpan.Days > 365) ? true : false;
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                if (null != productVM.fileToUpload &&
                    productVM.fileToUpload.Length > 0)
                {
                    // Find the filename extension of the file to be uploaded.
                    string fileExt = Path.GetExtension(
                    productVM.fileToUpload.FileName);
                    // Rename the uploaded file with the staff’s name.
                    string uploadedFile = productVM.ProductName + fileExt;
                    // Get the complete path to the images folder in server
                    string savePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot\\images", uploadedFile);
                    // Upload the file to server
                    using (var fileSteam = new FileStream(
                    savePath, FileMode.Create))
                    {
                        await productVM.fileToUpload.CopyToAsync(fileSteam);
                    }
                    productVM.ProductImage = uploadedFile;
                }
                productVM.updateProduct();
                return RedirectToAction("Index");
            }
            TimeSpan timeSpan = DateTime.Now - productVM.EffectiveDate;
            ViewData["Page"] = "UpdateProduct";
            ViewData["ShowisObsolete"] = (timeSpan.Days > 365) ? true : false;
            return View(productVM);
        }
    }
}
