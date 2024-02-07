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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace HobbyCollection.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly MainDbContext _context;
        private readonly Cloudinary _cloudinary;
        public FavoritesService(MainDbContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
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
            Favorite favorite = viewModel.Favorite;

            if (viewModel.ImageFile != null)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(viewModel.ImageFile.FileName, viewModel.ImageFile.OpenReadStream()),
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = true
                };
                var uploadResult = _cloudinary.Upload(uploadParams);
                var url = uploadResult.Url.ToString();
                favorite.Image = url;
            }

            // string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // favorite.UserId = userId;
            viewModel.TagsList = await getTagsList();
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