using SmartPlate.Application.DTOs.Snapshots;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Entities;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class RegisterUserBodyMetricsCase : IRegisterUserBodyMetricsCase{

    private readonly Context _db;

    public RegisterUserBodyMetricsCase(Context db)
    {
        _db = db;
    }
    public async Task ExecuteAsync(UserBodyMetricsSnapshot snapshot)
    {
        var userBodyMetrics = new UserBodyMetrics()
        {
            Id = Guid.NewGuid(),
            UserId = snapshot.UserId,
            MetricDate = snapshot.MetricDate,
            WeightKg = snapshot.WeightKg,
        };

        _db.Add(userBodyMetrics);
        await _db.SaveChangesAsync();
    }

}
