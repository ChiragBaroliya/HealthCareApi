using System.Collections.Generic;

namespace HealthCare.Application.DTOs
{
    public class HospitalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public List<DepartmentDto> Departments { get; set; } = new();
    }

    public class CreateHospitalDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }

    public class DepartmentDto
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
    }

    public class CreateDepartmentDto
    {
        public int HospitalId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
    }
}
