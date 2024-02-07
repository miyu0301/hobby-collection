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

namespace HobbyCollection.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly MainDbContext _context;

        public FavoritesService(MainDbContext context)
        {
            _context = context;
        }

        public async Task<List<SelectListItem>> getTagsList()
        {
            return await _context.Tag
                            .Where(t => !t.DeleteFlag)
                            .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                            .ToListAsync();
        }

        public async void insertFavorite(FavoriteCreateViewModel viewModel)
        {
            // string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // favorite.UserId = userId;
            viewModel.TagsList = await getTagsList();
            Favorite favorite = viewModel.Favorite;
            favorite.UserId = "dummy";
            favorite.CreateDate = DateTime.UtcNow;

            _context.Add(favorite);
            await _context.SaveChangesAsync();

            foreach (var tagId in viewModel.SelectedTagIds)
            {
                _context.Add(new FavoriteTagMappings
                {
                    FavoriteId = favorite.Id,
                    TagId = tagId,
                    CreateDate = DateTime.UtcNow
                });
            }
            await _context.SaveChangesAsync();
        }

    }
}