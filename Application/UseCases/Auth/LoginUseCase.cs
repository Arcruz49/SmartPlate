using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Application.Security;
using SmartPlate.Data;
using SmartPlate.Domain.Entities;
using SmartPlate.Domain.ValueObjects;

namespace SmartPlate.Application.UseCases;

public class LoginUseCase : ILoginUseCase{

    private readonly Context _db;
    private readonly PasswordHasher<Users> _passwordHasher;
    private readonly JwtTokenGenerator _tokenGenerator;


    public LoginUseCase(Context db, JwtTokenGenerator jwtTokenGenerator)
    {
        _db = db;
        _passwordHasher = new PasswordHasher<Users>();
        _tokenGenerator = jwtTokenGenerator;
    }
    public async Task<UserDto> ExecuteAsync(LoginRequest request)
    {
        var email = new Email(request.email);
        var password = new Password(request.password);

        var user = await _db.users.AsNoTracking().Where(a => a.email.Equals(email.Value)).FirstOrDefaultAsync() ?? throw new InvalidOperationException("Email não registrado.");
        
        var result = _passwordHasher.VerifyHashedPassword(user, user.password, password.Value);

        if(result == PasswordVerificationResult.Failed) throw new InvalidOperationException("Senha incorreta");

        var token = _tokenGenerator.GenerateToken(user.id.ToString(), user.name, user.email);

        return new UserDto(user.name, user.email, token);
    }
}
