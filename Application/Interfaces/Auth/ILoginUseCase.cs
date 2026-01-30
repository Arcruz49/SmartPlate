namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
public interface ILoginUseCase
{
    Task<UserDto> ExecuteAsync(LoginRequest request);
}