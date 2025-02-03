using MediatR;
using SeveClie.Application.Queries;
using SeveClie.Application.Services;

namespace SeveClie.Application.Handlers
{
    public class GetAllClieQueryHandler : IRequestHandler<GetAllClieQuery, IEnumerable<ClieDto>>
    {
        private readonly ClieService _clieService;

        public GetAllClieQueryHandler(ClieService clieService)
        {
            _clieService = clieService;
        }

        public async Task<IEnumerable<ClieDto>> Handle(GetAllClieQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clieList = await _clieService.GetAllClieAsync();
                return clieList; 
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the clients.", ex);
            }
        }
    }
}
