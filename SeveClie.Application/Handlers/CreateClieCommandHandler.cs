using MediatR;
using SeveClie.Application.Commands;
using SeveClie.Application.Services;

namespace SeveClie.Application.Handlers
{
    public class CreateClieCommandHandler : IRequestHandler<CreateClieCommand, ClieDto>
    {
        private readonly ClieService _clieService;

        public CreateClieCommandHandler(ClieService clieService)
        {
            _clieService = clieService;
        }

        public async Task<ClieDto> Handle(CreateClieCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clieDto = await _clieService.CreateClieAsync(request);

                return clieDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al crear el cliente.", ex);
            }
        }

    }
}
