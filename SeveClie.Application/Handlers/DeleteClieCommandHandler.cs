using MediatR;
using SeveClie.Infrastructure.Repositories;

public class DeleteClieCommandHandler : IRequestHandler<DeleteClieCommand, bool>
{
    private readonly IClieRepository _clieRepository;

    public DeleteClieCommandHandler(IClieRepository clieRepository)
    {
        _clieRepository = clieRepository;
    }

    public async Task<bool> Handle(DeleteClieCommand request, CancellationToken cancellationToken)
    {
        var clie = await _clieRepository.GetByIdAsync(request.IdClie);
        if (clie == null)
            return false;

        await _clieRepository.DeleteAsync(clie.IdClie);
        return true;
    }
}
