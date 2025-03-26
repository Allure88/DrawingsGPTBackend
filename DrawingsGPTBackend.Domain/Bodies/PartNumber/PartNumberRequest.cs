namespace DrawingsGPTBackend.Domain.Bodies.PartNumber;

public class PartNumberRequest
{
    public NodeDirectRqBody NodeDirectRqBody { get; set; } = new();
    public PartNumberOptions Options { get; set; } = null!;
}
