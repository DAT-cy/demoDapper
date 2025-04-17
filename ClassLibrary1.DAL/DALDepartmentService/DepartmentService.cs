using ClassLibrary1.DAL.DTOS.DepartmentDto;
using ClassLibrary1.DAL.DTOS.DepartmentModel;
using ClassLibrary1.DAL.DbConnection;
using Dapper;
using Microsoft.Extensions.Logging;

namespace ClassLibrary1.DAL.DALDepartmentService;

public class DepartmentService : IDepartmentService
{
    
    private readonly DbConnection.DbConnection _db;
    private readonly SqlQueryLoader _queryLoader;
    private readonly ILogger<DepartmentService> _logger;
    
    public DepartmentService(DbConnection.DbConnection db, SqlQueryLoader queryLoader ,ILogger<DepartmentService> logger )
    {
        _db = db;
        _queryLoader = queryLoader;
        _logger = logger;
        
    }
    public List<DepartmentDALDto> GetAllDepartments()
    {
        var query = _queryLoader.GetQueryFromXml("getAllDepartments");
        _logger.LogInformation("Thực hiện query: {query}", query);
        var result = _db.QueryWithConnection(conn =>
            conn.Query<DepartmentDALDto>(query).ToList());
        return result;
        
    }

    public int CreateDepartment(DepartmentDALModel department)
    {
        var query = _queryLoader.GetQueryFromXml("createDepartment");
        _logger.LogInformation("Thực hiện query: {query}", query);
        var result = _db.QueryWithConnection(conn =>
            conn.Execute(query, new
            {
                department.NameDepartment,
                department.Status,
                department.Code,
                department.Description
            }));
        _logger.LogInformation("Thêm thành công department: {department}", department.NameDepartment);
        return result;
    }

    public int UpdateDepartment(long departmentId, DepartmentDALModel department)
    {
        CheckDepartmentExist(departmentId);
        var query = _queryLoader.GetQueryFromXml("updateDepartment");
        _logger.LogInformation("Thực hiện query: {query}", query);
        var result = _db.QueryWithConnection(conn =>
            conn.Execute(query, new
            {
                department.NameDepartment,
                department.Status,
                department.Code,
                department.Description,
                Id = departmentId
            }));
        _logger.LogInformation("thực hiện update thành công department: {department}", department.NameDepartment);
        return result;
    }

    public long DeleteDepartment(long departmentId)
    {
        CheckDepartmentExist(departmentId);
        var query = _queryLoader.GetQueryFromXml("deleteDepartment");
        _logger.LogInformation("Thực hiện query: {query}", query);
        var result = _db.QueryWithConnection(conn =>
            conn.Execute(query, new
            {
                Id = departmentId
            })
        );
        return departmentId;
    }

    public DepartmentDALDto GetDetailDepartment(long departmentId)
    {
        DepartmentDALDto departmentDalDto = CheckDepartmentExist(departmentId);
        return departmentDalDto;
    }
    public DepartmentDALDto CheckDepartmentExist(long departmentId)
    {
        var query = _queryLoader.GetQueryFromXml("getDepartmentById");
        _logger.LogInformation("thực hiện query: {query}", query);
        var result = _db.QueryWithConnection(conn =>
            conn.QueryFirstOrDefault<DepartmentDALDto>(query, new { Id = departmentId }));
        _logger.LogInformation("Tìm thấy department với ID: {departmentId}", departmentId);
        if (result == null)
        {
            throw new ErrorResponse(ErrorCode.DEPARTMENT_NOT_FOUND);
        }

        return result;
    }
}