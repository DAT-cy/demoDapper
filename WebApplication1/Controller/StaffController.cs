using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Model;
using WebApplication1.Service.StaffService;

namespace WebApplication1.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService staffService;

        public StaffController(IStaffService staffService)
        {
            this.staffService = staffService;
        }

        [HttpPost]
        public async Task<ActionResult<StaffDto>> CreateDepartment([FromBody] StaffModel staffModel)
        {
            var staffDto = await staffService.AddStaff(staffModel);
            return Ok(staffDto); // Bọc trong Ok()
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<ActionResult<List<StaffDto>>> GetALLDepartment()
        {
            var staffDtos = await staffService.GetAllStaff();
            return Ok(staffDtos);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateDepartment(long id, [FromBody] StaffModel staffModel)
        {
            var update = await staffService.UpdateStaff(id, staffModel); // ← await
            return Ok(update); // bọc trong Ok()
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteDepartment(long id)
        {
            var delete = await staffService.DeleteStaff(id); // ← await
            return Ok(delete); // bọc trong Ok()
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetDetailDepartment(long id)
        {
            var staffDto = await staffService.GetStaffById(id); // ← await
            return Ok(staffDto); // bọc trong Ok()
        }

        
        [HttpPut("[action]")]
        public async Task<ActionResult<List<StaffDto>>> GetAllStaff([FromBody] List<StaffDepartmentModel> staffDepartmentModel)
        {
            var staffDtos = await staffService.ChoseStaffForDepartment(staffDepartmentModel);
            return Ok(staffDtos);
        }
        
        [HttpGet("by-department/{departmentId}")]
        public async Task<ActionResult<List<StaffDto>>> GetAllStaffByDepartMentId(long departmentId)
        {
            List<StaffDto> staffDtos = await staffService.GetStaffByDepartmentId(departmentId);
            return Ok(staffDtos);
        }
        
    }
}