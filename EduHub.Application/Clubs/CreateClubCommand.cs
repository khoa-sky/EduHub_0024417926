using System.Security.Cryptography;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;
using Microsoft.VisualBasic;

namespace EduHub.Application.Clubs;

public record CreateClubCommand(string Name, string Description);

public class CreateClubCommandHandler
{
    private const int MaxNameLength = 50;
    private readonly IClubRepository _repository;

    public CreateClubCommandHandler(IClubRepository repository)
    {
        _repository = repository;
    }

    public async Task<Club> HandleAsync(CreateClubCommand command, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            throw new ArgumentException($"Tên câu lạc bộ không đc để trống");

        if (command.Name.Length > MaxNameLength)
            throw new ArgumentException($"Tên câu lạc bộ tối đa {MaxNameLength} ký tự.");

        var club = Club.Create(command.Name.Trim(), command.Description ?? string.Empty);
        await _repository.AddAsync(club, ct);
        return club;
    }
}
