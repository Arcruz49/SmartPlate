using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Application.Security;
using SmartPlate.Infrastructure.Data;
using SmartPlate.Domain.Entities;
using SmartPlate.Domain.ValueObjects;

namespace SmartPlate.Application.UseCases;

public class RegisterUserUseCase : IRegisterUserUseCase{

    private readonly Context _db;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly JwtTokenGenerator _tokenGenerator;


    public RegisterUserUseCase(Context db, JwtTokenGenerator jwtTokenGenerator)
    {
        _db = db;
        _passwordHasher = new PasswordHasher<User>();
        _tokenGenerator = jwtTokenGenerator;
    }
    public async Task<UserDto> ExecuteAsync(RegisterUserRequest request)
    {
        var email = new Email(request.Email);
        var password = new Password(request.Password);

        if (_db.users.AsNoTracking().Any(u => u.Email == email.Value))
            throw new InvalidOperationException("Email já cadastrado.");

        User user = new User()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = email.Value,
        };

        user.Password = _passwordHasher.HashPassword(user, password.Value);

        _db.users.Add(user);

        await _db.SaveChangesAsync();

        var token = _tokenGenerator.GenerateToken(user.Id.ToString(), user.Name, user.Email);


        return new UserDto(user.Name, user.Email, token);
    }
}
