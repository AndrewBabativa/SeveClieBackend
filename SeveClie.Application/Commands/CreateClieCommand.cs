using MediatR;

namespace SeveClie.Application.Commands
{
    public class CreateClieCommand : IRequest<ClieDto>
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNac { get; set; }
        public string EstadoCivil { get; set; }
    }
}
