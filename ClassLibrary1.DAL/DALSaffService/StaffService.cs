 using ClassLibrary1.DAL.DbConnection;
 using ClassLibrary1.DAL.DTOS;
 using ClassLibrary1.DAL.DALService;
 using Dapper;
 using Microsoft.Extensions.Logging;


 namespace WorkerService1.DAL.DALService;

public class StaffService : IStaffService
{
    private readonly DbConnection _db;
    private readonly SqlQueryLoader _queryLoader;
    private readonly ILogger<StaffService> _logger;


    public StaffService(DbConnection db, SqlQueryLoader queryLoader ,ILogger<StaffService> logger )
    {
        _db = db;
        _queryLoader = queryLoader;
        _logger = logger;
        
    }

    public async Task<bool> DeleteStaff(long staffId)
    {
        try
        {
            // Lấy câu truy vấn DELETE từ file XML
            var query = _queryLoader.GetQueryFromXml("deleteUser");
            _logger.LogInformation("Thực hiện query: {query}", query);

            // Thực thi câu truy vấn DELETE với Dapper
            var result = await _db.QueryWithConnectionAsync(conn =>
                conn.ExecuteAsync(query, new { StaffId = staffId }));

            // Nếu result > 0, tức là đã xóa thành công
            if (result > 0)
            {
                _logger.LogInformation("Đã xóa nhân viên với ID: {staffId}", staffId);
                return true;
            }

            // Trường hợp không có bản ghi nào bị xóa
            _logger.LogWarning("Không tìm thấy nhân viên với ID: {staffId} để xóa", staffId);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi thực hiện query xóa nhân viên: {message}", ex.Message);
            throw;
        }
    }


    public async Task<IEnumerable<Staff>> GetAllStaffAsync()
    {
        try
        {
            var query = _queryLoader.GetQueryFromXml("getAllUsers");
            _logger.LogInformation("thưc hiện query {query}", query);

            var result = await _db.QueryWithConnectionAsync(conn =>
                conn.QueryAsync<Staff>(query));

            _logger.LogInformation("có từng này kết quar ", result.Count());
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ỗi khi thực hiện query {query}", ex.Message);
            throw;
        }
    }
    
    public async Task<bool> InsertStaffAsync(StaffInsertDto staff)
    {
        try
        {
            var query = _queryLoader.GetQueryFromXml("insertUser");
            _logger.LogInformation("Thực hiện insert với query: {query}", query);

            var result = await _db.QueryWithConnectionAsync(conn =>
                conn.ExecuteAsync(query, new
                {
                    staff.FullName,
                    staff.Position,
                    staff.Phone,
                    staff.Address,
                    staff.Age,
                    staff.Salary,
                    staff.Email,
                }));

            if (result > 0)
            {
                _logger.LogInformation("Insert nhân viên thành công: {name}", staff.FullName);
                return true;
            }

            _logger.LogWarning("Insert thất bại cho nhân viên: {name}", staff.FullName);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi insert nhân viên: {message}", ex.Message);
            throw;
        }
    }
    
    public async Task<bool> UpdateStaffAsync( long staffId , StaffInsertDto staff)
    {
        try
        {
            var query = _queryLoader.GetQueryFromXml("updateUser");
            _logger.LogInformation("Thực hiện update với query: {query}", query);

            var result = await _db.QueryWithConnectionAsync(conn =>
                conn.ExecuteAsync(query, new
                {
                    Id = staffId,
                    staff.FullName,
                    staff.Position,
                    staff.Phone,
                    staff.Address,
                    staff.Age,
                    staff.Salary,
                    staff.Email
                }));

            if (result > 0)
            {
                _logger.LogInformation("Update nhân viên thành công: {name}", staff.FullName);
                return true;
            }

            _logger.LogWarning("Update thất bại cho nhân viên: {name}", staff.FullName);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi update nhân viên: {message}", ex.Message);
            throw;
        }
    }
    public Staff TryGetStaffById(long staffId)
    {
        try
        {
            var query = _queryLoader.GetQueryFromXml("getUserById");
            _logger.LogInformation("Thực hiện query: {query}", query);

            var staff = _db.QueryWithConnection(conn =>
                conn.QueryFirstOrDefault<Staff>(query, new { Id = staffId }));

            if (staff != null)
            {
                _logger.LogInformation("Tìm thấy nhân viên với ID: {staffId}", staffId);
                return staff; 
            }

            _logger.LogWarning("Không tìm thấy nhân viên với ID: {staffId}", staffId);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi lấy thông tin nhân viên: {message}", ex.Message);
            return null; 
        }
    }
    
    public int ChosenStaff(StaffDepartmentDto staffDepartment)
    {
        
        var staff = TryGetStaffById(staffDepartment.StaffId);
        if (staff== null)
        {
            throw new ErrorResponse(ErrorCode.STAFF_NOT_FOUND);
        }        
        
        var query = _queryLoader.GetQueryFromXml("insertStafDepartment");
        _logger.LogInformation("Thực hiện query: {query}", query);
        var result = _db.QueryWithConnection(conn =>
            conn.Execute(query, new
            {
                staffDepartment.DepartmentId,
                Id = staffDepartment.StaffId
            }));
        return result;

    }


    
    



}