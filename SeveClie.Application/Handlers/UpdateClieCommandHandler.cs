using MediatR;
using SeveClie.Application.Commands;
using SeveClie.Application.Services;
using System;

namespace SeveClie.Application.Handlers
{
    public class UpdateClieCommandHandler : IRequestHandler<UpdateClieCommand, ClieDto>
    {
        private readonly ClieService _clieService;

        public UpdateClieCommandHandler(ClieService clieService)
        {
            _clieService = clieService;
        }

        public async Task<ClieDto> Handle(UpdateClieCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _clieService.UpdateClieAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the client.", ex);
            }
        }
    }
}
