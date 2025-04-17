namespace WebApplication1.Dto
{
    public class StaffDto
    {
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}