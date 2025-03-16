namespace DrawingsGPTBackend.Domain.Bodies.PartNumber;

public class NodeDirectRqBody(string partNumberExist, string fullFileName, NodeDirectRqBody? parent)
{
    public string PartNumberFull { get; set; } = partNumberExist;
    public bool IsDetail => Childs.Count == 0;
    public List<NodeDirectRqBody> Childs { get; } = [];
    public string FullFileName { get; } = fullFileName;
    public NodeDirectRqBody? Parent { get; } = parent;
    public int Level { get; set; }
    public int? SettedNumber { get; set; }
}
