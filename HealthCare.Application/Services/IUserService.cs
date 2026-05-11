using HealthCare.Application.DTOs;
using HealthCare.Application.Models;
using System.Threading.Tasks;

namespace HealthCare.Application.Services
{
    public interface IUserService
    {
        Task<ResponseModel<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    }
}
