﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimpusProject.Models;

namespace TimpusProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        private readonly TimpusDBContext _context;
        public INotyfService _notifyService { get; }

        public AuthorsController(TimpusDBContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/Authors
        public async Task<IActionResult> Index()
        {
            var LoggedaccountID = HttpContext.Session.GetString("AccountId");
            if (string.IsNullOrEmpty(LoggedaccountID))
            {
                _notifyService.Warning("You need to login with admin account");
                return RedirectToAction("Login", "LoginAdmin");
            }
            int Loggedid = Int32.Parse(LoggedaccountID);
            var LoggedAccount = _context.Accounts
                    .AsNoTracking()
                    .Include(account => account.Role)
                    .FirstOrDefault(account => account.AccountId == Loggedid);
            ViewBag.Account = LoggedAccount;

            return View(await _context.Authors.ToListAsync());
        }

        // GET: Admin/Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var LoggedaccountID = HttpContext.Session.GetString("AccountId");
            if (string.IsNullOrEmpty(LoggedaccountID))
            {
                _notifyService.Warning("You need to login with admin account");
                return RedirectToAction("Login", "LoginAdmin");
            }
            int Loggedid = Int32.Parse(LoggedaccountID);
            var LoggedAccount = _context.Accounts
                    .AsNoTracking()
                    .Include(account => account.Role)
                    .FirstOrDefault(account => account.AccountId == Loggedid);
            ViewBag.Account = LoggedAccount;

            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Admin/Authors/Create
        public IActionResult Create()
        {
            var LoggedaccountID = HttpContext.Session.GetString("AccountId");
            if (string.IsNullOrEmpty(LoggedaccountID))
            {
                _notifyService.Warning("You need to login with admin account");
                return RedirectToAction("Login", "LoginAdmin");
            }
            int Loggedid = Int32.Parse(LoggedaccountID);
            var LoggedAccount = _context.Accounts
                    .AsNoTracking()
                    .Include(account => account.Role)
                    .FirstOrDefault(account => account.AccountId == Loggedid);
            ViewBag.Account = LoggedAccount;

            return View();
        }

        // POST: Admin/Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,AuthorName,Biography,Avatar")] Author author)
        {
            var LoggedaccountID = HttpContext.Session.GetString("AccountId");
            if (string.IsNullOrEmpty(LoggedaccountID))
            {
                _notifyService.Warning("You need to login with admin account");
                return RedirectToAction("Login", "LoginAdmin");
            }
            int Loggedid = Int32.Parse(LoggedaccountID);
            var LoggedAccount = _context.Accounts
                    .AsNoTracking()
                    .Include(account => account.Role)
                    .FirstOrDefault(account => account.AccountId == Loggedid);
            ViewBag.Account = LoggedAccount;

            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                _notifyService.Success("Create successfully!");
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Admin/Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var LoggedaccountID = HttpContext.Session.GetString("AccountId");
            if (string.IsNullOrEmpty(LoggedaccountID))
            {
                _notifyService.Warning("You need to login with admin account");
                return RedirectToAction("Login", "LoginAdmin");
            }
            int Loggedid = Int32.Parse(LoggedaccountID);
            var LoggedAccount = _context.Accounts
                    .AsNoTracking()
                    .Include(account => account.Role)
                    .FirstOrDefault(account => account.AccountId == Loggedid);
            ViewBag.Account = LoggedAccount;


            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Admin/Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,AuthorName,Biography,Avatar")] Author author)
        {
            var LoggedaccountID = HttpContext.Session.GetString("AccountId");
            if (string.IsNullOrEmpty(LoggedaccountID))
            {
                _notifyService.Warning("You need to login with admin account");
                return RedirectToAction("Login", "LoginAdmin");
            }
            int Loggedid = Int32.Parse(LoggedaccountID);
            var LoggedAccount = _context.Accounts
                    .AsNoTracking()
                    .Include(account => account.Role)
                    .FirstOrDefault(account => account.AccountId == Loggedid);
            ViewBag.Account = LoggedAccount;

            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Edit successfully!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorId))
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
            return View(author);
        }

        // GET: Admin/Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var LoggedaccountID = HttpContext.Session.GetString("AccountId");
            if (string.IsNullOrEmpty(LoggedaccountID))
            {
                _notifyService.Warning("You need to login with admin account");
                return RedirectToAction("Login", "LoginAdmin");
            }
            int Loggedid = Int32.Parse(LoggedaccountID);
            var LoggedAccount = _context.Accounts
                    .AsNoTracking()
                    .Include(account => account.Role)
                    .FirstOrDefault(account => account.AccountId == Loggedid);
            ViewBag.Account = LoggedAccount;

            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Admin/Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            _notifyService.Success("Delete successfully!");
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
