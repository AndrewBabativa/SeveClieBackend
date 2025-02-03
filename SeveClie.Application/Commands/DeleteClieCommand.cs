using MediatR;

public class DeleteClieCommand : IRequest<bool>
{
    public int IdClie { get; set; }

    public DeleteClieCommand(int id)
    {
        IdClie = id;
    }
}
