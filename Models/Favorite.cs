using System.ComponentModel.DataAnnotations;

namespace HobbyCollection.Models;

public class Favorite
{
  public int Id { get; set; }
  public string UserId { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public decimal? Price { get; set; }
  public string? Image { get; set; }
  public DateTime CreateDate { get; set; }
  public DateTime? UpdateDate { get; set; }
}