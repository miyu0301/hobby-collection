using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HobbyCollection.Models;

public class FavoriteCreateViewModel
{
    public Favorite Favorite { get; set; }
    public IFormFile? ImageFile { get; set; }
    public List<SelectListItem> TagsList { get; set; }
    public List<int> SelectedTagIds { get; set; } = new List<int>();

}
