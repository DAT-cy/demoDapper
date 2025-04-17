using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entity;

[Table("tbl_staff")]
public class StaffEntity
{
    [Key]
    [Column("id_staff")]
    public long Id { get; set; }

    [Required]
    [Column("full_name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Column("position")]
    public string Position { get; set; } = string.Empty;

    [Required]
    [Column("phone_number")]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [Column("address")]
    public string Address { get; set; } = string.Empty;

    [Required]
    [Column("age")]
    public int Age { get; set; }

    [Required]
    [Column("salary")]
    public decimal Salary { get; set; }

    [Required]
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Column("date_of_join")]
    public DateTime DateOfJoin { get; set; } = DateTime.Now;

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
    
    [Column("department_id")]
    public long DepartmentId { get; set; }
}