namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
public interface IRegisterUserUseCase
{
    Task<UserDto> ExecuteAsync(RegisterUserRequest request);
}