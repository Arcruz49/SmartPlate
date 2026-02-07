using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class GetUserMetricsCase : IGetUserMetricsCase{

    private readonly Context _db;

    public GetUserMetricsCase(Context db)
    {
        _db = db;
    }
    public async Task<List<UserBodyMetricsResponse>> ExecuteAsync(Guid userId)
    {
        return await _db.UserBodyMetrics
            .AsNoTracking()
            .Where(m => m.UserId == userId)
            .OrderBy(m => m.MetricDate)
            .Select(m => new UserBodyMetricsResponse(
                m.WeightKg ?? 0,
                m.MetricDate
            ))
            .ToListAsync();
    }

}
