using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly MVCContext _context;

        // GET: /<controller>/

        public ProductController(MVCContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var result = await _context.Products.ToListAsync();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            product.InsertDate = DateTime.UtcNow;

            _context.Products.Add(product);

            await _context.SaveChangesAsync(token);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var phone = await _context.Products.FindAsync(id);
            if (phone == null)
            {
                return RedirectToAction("Index");
            }

            return View(phone);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var phone = await _context.Products.FindAsync(id);
            if (phone == null)
            {
                return RedirectToAction("Index");
            }
            _context.Products.Remove(phone);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product phoneModel)
        {
            if (!ModelState.IsValid)
            {
                return View(phoneModel);
            }

            var phone = await _context.Products.FindAsync(phoneModel.Id);

            if (phoneModel == null)
            {
                return RedirectToAction("Index");
            }

            phone.Cost = phoneModel.Cost;
            phone.Type = phoneModel.Type;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
