using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Clubs;

public record GetClubsQuery;

public class GetClubsQueryHandler
{
    private readonly IClubRepository _repository;

    public GetClubsQueryHandler(IClubRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Club>> HandleAsync(GetClubsQuery query, CancellationToken ct = default)
    {
        return await _repository.GetAllAsync(ct);
    }
}
