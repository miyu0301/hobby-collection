using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HobbyCollection.Models;

namespace HobbyCollection.Services
{
    public interface IFavoritesService
    {
        Task<List<SelectListItem>> getTagsList();
        void insertFavorite(FavoriteCreateViewModel viewModel);
    }
}