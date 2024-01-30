using System.ComponentModel.DataAnnotations;

namespace HobbyCollection.Models;

public class User
{
  public int Id { get; set; }
  public required string UserName { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }
  public DateTime CreateDate { get; set; }
  public DateTime? UpdateDate { get; set; }
}