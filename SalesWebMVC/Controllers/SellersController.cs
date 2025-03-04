﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SalesWebMVCContext _context;

        public SellersController(SalesWebMVCContext context)
        {
            _context = context;
        }

        // GET: Sellers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seller.ToListAsync());
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Seller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Obtém todos os departamentos para popular o dropdown
            var departments = await _context.Departament.ToListAsync();

            if (departments == null || !departments.Any())
            {
                // Lógica para lidar com o caso de não haver departamentos
                ViewBag.ErrorMessage = "No departments found.";
                return View();
            }

            ViewBag.DepartmentList = departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(), // ID do departamento como valor do item
                Text = d.Name // Nome do departamento como texto exibido no dropdown
            }).ToList();

            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,BirthDate,BaseSalary,DepartamentId")] Seller seller)
        {
            seller.Departament = await _context.Departament.FindAsync(seller.DepartamentId);
            if (ModelState.IsValid)
            {
                
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Recarregar a lista de departamentos caso o modelo não seja válido
            var departments = await _context.Departament.ToListAsync();

            if (departments == null || !departments.Any())
            {
                // Lógica para lidar com o caso de não haver departamentos
                ViewBag.ErrorMessage = "No departments found.";
                return View();
            }

            ViewBag.DepartmentList = departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(), // ID do departamento como valor do item
                Text = d.Name // Nome do departamento como texto exibido no dropdown
            }).ToList();

            return View(seller);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Seller.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,BirthDate,BaseSalary")] Seller seller)
        {
            if (id != seller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Seller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seller = await _context.Seller.FindAsync(id);
            if (seller != null)
            {
                _context.Seller.Remove(seller);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
            return _context.Seller.Any(e => e.Id == id);
        }
    }
}
