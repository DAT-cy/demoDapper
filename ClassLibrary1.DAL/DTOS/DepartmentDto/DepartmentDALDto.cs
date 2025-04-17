namespace ClassLibrary1.DAL.DTOS.DepartmentDto;

public class DepartmentDALDto
{
    public long Id { get; set; }
    public string NameDepartment { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}