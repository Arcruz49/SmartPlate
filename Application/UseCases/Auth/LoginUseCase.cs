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

public class LoginUseCase : ILoginUseCase{

    private readonly Context _db;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly JwtTokenGenerator _tokenGenerator;


    public LoginUseCase(Context db, JwtTokenGenerator jwtTokenGenerator)
    {
        _db = db;
        _passwordHasher = new PasswordHasher<User>();
        _tokenGenerator = jwtTokenGenerator;
    }
    public async Task<UserDto> ExecuteAsync(LoginRequest request)
    {
        var email = new Email(request.Email);
        var password = new Password(request.Password);

        var user = await _db.Users.AsNoTracking().Where(a => a.Email.Equals(email.Value)).FirstOrDefaultAsync() ?? throw new InvalidOperationException("Email não registrado.");
        
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password.Value);

        if(result == PasswordVerificationResult.Failed) throw new InvalidOperationException("Senha incorreta");

        var token = _tokenGenerator.GenerateToken(user.Id.ToString(), user.Name, user.Email);

        return new UserDto(user.Name, user.Email, token);
    }
}
