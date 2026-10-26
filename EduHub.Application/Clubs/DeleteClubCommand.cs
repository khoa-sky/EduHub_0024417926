using EduHub.Domain.Interfaces;

namespace EduHub.Application.Clubs;

public record DeleteClubCommand(Guid Id);

public class DeleteClubCommandHandler
{
    private readonly IClubRepository _repository;

    public DeleteClubCommandHandler(IClubRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DeleteClubCommand command, CancellationToken ct = default)
    {
        await _repository.DeleteAsync(command.Id, ct);
    }
}
