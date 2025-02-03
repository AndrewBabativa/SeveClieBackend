using MediatR;
using SeveClie.Application.DTOs;

namespace SeveClie.Application.Queries
{
    public class GetAllClieQuery : IRequest<IEnumerable<ClieDto>>
    {
    }
}
