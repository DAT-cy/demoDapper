using WebApplication1.Dto;
using WebApplication1.Entity;
using WebApplication1.Model;
using WebApplication1.Repository;

namespace WebApplication1.Service.StaffService;

public class StaffService : IStaffService
{
    private readonly StaffRepository staffRepository;
    private readonly DepartmentRepository departmentRepository;

    public StaffService(StaffRepository staffRepository , DepartmentRepository departmentRepository)
    {
        this.staffRepository = staffRepository;
        this.departmentRepository = departmentRepository;
    }
    
    public async Task<List<StaffDto>> GetAllStaff()
    {
        List<StaffEntity> staffEntity = await staffRepository.GetAllStaffAsync();
        List<StaffDto> staffDtos = staffEntity.Select(e => Mapper.StaffMapper.ToDto(e)).ToList();
        return staffDtos;
        
    }

    public async Task<Boolean> ChoseStaffForDepartment(List<StaffDepartmentModel> staffDepartmentModel)
    {

        foreach (var item in staffDepartmentModel)
        {
            staffRepository.ChosenStaff(item.StaffId , item.DepartmentId);
            return true;
        }
        return false;
    }
    
    public  async Task<StaffDto> GetStaffById(long id)
    {
        StaffEntity staffEntity =  staffRepository.GetStaffById(id);
        StaffDto staffDto = Mapper.StaffMapper.ToDto(staffEntity);
        return staffDto;
    }

    public async Task<StaffDto> AddStaff(StaffModel staff)
    {
        StaffEntity staffEntity = Mapper.StaffMapper.ToEntity(staff);
        await staffRepository.InsertStaff(staffEntity); 

        StaffDto staffDto = Mapper.StaffMapper.ToDto(staffEntity);
        return staffDto;
    }

    public async Task<bool> UpdateStaff(long staffId,StaffModel staff)
    {
        StaffEntity staffEntity =  staffRepository.GetStaffById(staffId);
        if (staffEntity == null)
        {
            throw new NullReferenceException();
        }
        staffRepository.updateStaff(staffEntity);
        return true;
    }

    public   async Task<bool>  DeleteStaff(long id)
    {
        StaffEntity staffEntity =  staffRepository.GetStaffById(id);
        if (staffEntity == null)
        {
            throw new NullReferenceException();
        }
        staffEntity.IsDeleted = true;
        staffRepository.updateStaff(staffEntity);
        return true;
    }
    
    public async Task<List<StaffDto>> GetStaffByDepartmentId(long departmentId )
    {
        DepartmentEntity departmentEntity = departmentRepository.FindByDepartmentId(departmentId);
        if (departmentEntity == null)
        {
            throw new InvalidDataException($"Department with ID {departmentId} not found.");
        }
        List<StaffEntity> staffEntities = staffRepository.GetStaffByDepartmentId(departmentId);
        
        List<StaffDto> staffDtos = staffEntities.Select(e => Mapper.StaffMapper.ToDto(e)).ToList();
        return staffDtos;
    }
}