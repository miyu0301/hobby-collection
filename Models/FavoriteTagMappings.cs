using System.ComponentModel.DataAnnotations;

namespace HobbyCollection.Models;

public class FavoriteTagMappings
{
  public int Id { get; set; }
  public int FavoriteId { get; set; }
  public int TagId { get; set; }
  public DateTime CreateDate { get; set; }
  public DateTime? UpdateDate { get; set; }
  public Tag Tag { get; set; }
}