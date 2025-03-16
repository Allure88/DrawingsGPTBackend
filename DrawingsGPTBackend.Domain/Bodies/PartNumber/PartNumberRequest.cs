namespace DrawingsGPTBackend.Domain.Bodies.PartNumber;

public class PartNumberRequest(NodeDirectRqBody nodeDirectRqBody, PartNumberOptions options)
{
    public NodeDirectRqBody NodeDirectRqBody { get; } = nodeDirectRqBody;
    public PartNumberOptions Options { get; } = options;
}
