using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entity;

public class UserEntity
{
    [Key]
    [Column("user_id")]
    public long Id { get; set; }
    
    [Column("username")]
    public string UserName { get; set; } = string.Empty;
    
    [Column("password")]
    public string Password { get; set; } = string.Empty;
    
}