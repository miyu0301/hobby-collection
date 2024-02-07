using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HobbyCollection.Data;
using HobbyCollection.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using HobbyCollection.Services;

namespace HobbyCollection.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly MainDbContext _context;
        private readonly IFavoritesService _service;
        private readonly ILogger<HomeController> _logger;

        public FavoritesController(MainDbContext context, IFavoritesService service, ILogger<HomeController> logger)
        {
            _context = context;
            _service = service;
            _logger = logger;
        }

        // GET: Favorites
        public async Task<IActionResult> Index()
        {
            return View(await _context.Favorite.ToListAsync());
        }

        // GET: Favorites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favorite == null)
            {
                return NotFound();
            }

            var tags = await _context.FavoriteTagMappings
                .Where(m => m.FavoriteId == id)
                .Select(m => m.Tag)
                .ToListAsync();

            var viewModel = new FavoriteDetailsViewModel
            {
                Favorite = favorite,
                TagsList = tags
            };

            return View(viewModel);
        }

        // GET: Favorites/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new FavoriteCreateViewModel
            {
                TagsList = await _service.getTagsList()
            };

            return View(viewModel);
        }

        // POST: Favorites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FavoriteCreateViewModel viewModel)
        {
            // prevent be validated before insert
            ModelState.Remove("Favorite.UserId");
            ModelState.Remove("TagsList");
            if (ModelState.IsValid)
            {
                _service.insertFavorite(viewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Count > 0)
                    {
                        Console.WriteLine($"{entry.Key}: {string.Join(", ", entry.Value.Errors.Select(e => e.ErrorMessage))}");
                    }
                }
            }
            return View(viewModel);
        }

        // GET: Favorites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorite.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Name,Description,Price,Image")] Favorite favoriteToUpdate)
        {
            if (id != favoriteToUpdate.Id)
            {
                return NotFound();
            }

            // prevent be validated before insert
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                try
                {
                    var favorite = await _context.Favorite.FindAsync(id);
                    if (favorite == null)
                    {
                        return NotFound();
                    }

                    favorite.Name = favoriteToUpdate.Name;
                    favorite.Description = favoriteToUpdate.Description;
                    favorite.Price = favoriteToUpdate.Price;
                    favorite.Image = favoriteToUpdate.Image;
                    favorite.UpdateDate = DateTime.UtcNow;
                    _context.Update(favorite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteExists(favoriteToUpdate.Id))
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
            return View(favoriteToUpdate);
        }

        // GET: Favorites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favorite == null)
            {
                return NotFound();
            }

            return View(favorite);
        }

        // POST: Favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favorite = await _context.Favorite.FindAsync(id);
            if (favorite != null)
            {
                _context.Favorite.Remove(favorite);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteExists(int id)
        {
            return _context.Favorite.Any(e => e.Id == id);
        }
    }
}
