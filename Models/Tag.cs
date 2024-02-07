using System.ComponentModel.DataAnnotations;

namespace HobbyCollection.Models;

public class Tag
{
  public int Id { get; set; }
  public string UserId { get; set; }
  public required string Name { get; set; }
  public bool DeleteFlag { get; set; } = false;
  public DateTime CreateDate { get; set; }
  public DateTime? UpdateDate { get; set; }
}