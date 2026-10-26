using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

/// <summary>
/// Kho CLB lưu tạm trong bộ nhớ — dùng cho Chương 3 khi chưa có database.
/// Chương 4 sẽ thay bằng bản dùng EF Core và PostgreSQL.
/// </summary>
public class InMemoryClubRepository : IClubRepository
{
    // static: repository đăng ký Scoped (mỗi request một bản mới) nhưng dữ liệu phải sống suốt vòng đời ứng dụng.
    private static readonly List<Club> _clubs = new();

    public Task<IEnumerable<Club>> GetAllAsync(CancellationToken ct = default)
    {
        lock (_lock)
            return Task.FromResult<IEnumerable<Club>>(_clubs.ToList);
    }

    public Task<Club?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        lock (_lock)
            return Task.FromResult(_clubs.FirstOrDefault(c => c.Id == id));
    }

    public Task AddAsync(Club club, CancellationToken ct = default)
    {
        lock (_lock) _clubs.Add(club);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Club club, CancellationToken ct = default)
    {
        lock (_lock)
        {
            var index = _clubs.FindIndex(c => c.Id == club.Id);
            if (index < 0)
                throw new KeyNotFoundException($"không tìm thấy club {club.Id}");
            _clubs[index] = club;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        lock (_lock)
        {
            var club = _clubs.FirstOrDefault(c => c.Id == id)
                ?? throw new KeyNotFoundException($"không tìm thấy  club {id}");
            _clubs.Remove(club);
        }
        return Task.CompletedTask;
    }
}
