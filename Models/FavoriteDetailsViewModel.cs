using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HobbyCollection.Models;

public class FavoriteDetailsViewModel
{
    public Favorite Favorite { get; set; }
    public List<Tag> TagsList { get; set; }
}
